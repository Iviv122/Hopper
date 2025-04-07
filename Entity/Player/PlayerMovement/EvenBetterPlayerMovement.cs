using System;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private InputManager input;
    [SerializeField] private Transform orientation;
    [SerializeField] public Rigidbody rb;
    [Header("Walking")]
    [SerializeField] private float Speed;
    [SerializeField] private float MaxGroundSpeed = 25;
    [SerializeField] private float MaxAirSpeed = 25;
    [SerializeField] private float RunSpeed;
    [SerializeField] private float MoveSpeed = 15;
    [SerializeField] private float stopSpeed = 5; // static?
    [SerializeField] private float friction = 0.8f; // kinetic
    [SerializeField] private float accel = 3;
    [Header("Ground Check")]
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool canJump;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float height;
    [Header("Input")]
    [SerializeField] private Vector3 wishvel;
    [SerializeField] public Vector3 wishdir;
    [Header("Jump")]
    [SerializeField] private float jumpForce;

    [SerializeField] private float jumpBufferTime = 0.2f; // Buffer window duration
    private float jumpBufferCounter = 0f;
    [Header("Steps")]
    [SerializeField] private GameObject stepArrayUp;
    [SerializeField] private GameObject stepArrayDown;
    [SerializeField] private float stepSmooth;
    [SerializeField] RaycastHit slopeHit;
    
    [Header("Bools")]
    public bool IsOnWall = false; 
    [Header("Sounds")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] footSteps;

    public event Action groundTouched;

    private float GroundRunSpeed;
    private float AirRunSpeed;
    private float AccelRun;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        
        input.Jump += JumpInput;

        GroundRunSpeed = MaxGroundSpeed + RunSpeed;
        AirRunSpeed = MaxAirSpeed + RunSpeed;
        AccelRun = accel + 1;


    }
    private void Accelerate(float wishspeed)
    {
        float currentSpeed = Vector3.Dot(rb.linearVelocity, wishdir);
        float addSpeed = wishspeed - currentSpeed;
        
        if(addSpeed <= 0){
            return ;
        }
        float accelSpeed = accel*Time.deltaTime*wishspeed;
        if(accelSpeed > addSpeed){
            accelSpeed = addSpeed;
        } 
        rb.linearVelocity += accelSpeed*wishdir;
    }
    private void AirAccelerate(float wishspeed)
    {
        if(wishspeed > MaxAirSpeed){
            wishspeed = MaxAirSpeed;
        }
        float currSpeed = Vector3.Dot(rb.linearVelocity,wishdir);
        float addSpeed = wishspeed - currSpeed;
        if(addSpeed <= 0){
            return;
        } 
        float accelSpeed = accel*wishspeed*Time.deltaTime;
        if(accelSpeed > addSpeed){
            accelSpeed = addSpeed;
        }
        rb.linearVelocity += accelSpeed*wishdir;
    }
    private void Move()
    {
        
        wishvel = (orientation.forward * input.y + orientation.right * input.x).normalized;
        wishdir = new Vector3(wishvel.x,0,wishvel.z);
        wishdir = Vector3.ProjectOnPlane(wishdir,slopeHit.normal)*MoveSpeed;

        wishdir = Vector3.ProjectOnPlane(wishdir,slopeHit.normal);

        float wishspeed = wishdir.magnitude;
        wishdir.Normalize();
        
        if(wishspeed > MaxGroundSpeed){
            wishvel *= MaxGroundSpeed/wishspeed;
            wishspeed = MaxGroundSpeed;
        }

        if(isGrounded){
            Accelerate(wishspeed);
            groundTouched?.Invoke();
        }else{
            AirAccelerate(wishspeed);
        }
        
    }
    
    private void Friction()
    {
        if(isGrounded){
            float speed = Mathf.Sqrt(rb.linearVelocity.z * rb.linearVelocity.z + rb.linearVelocity.x * rb.linearVelocity.x);
            
            if (speed < 1)
            {
                rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
                return ;
            }
            float control = (speed < stopSpeed) ? stopSpeed : speed;
            float newspeed = control*friction*Time.deltaTime;
            
            float drop=0;
            drop += control*friction*Time.deltaTime;
            newspeed = speed - drop;
            newspeed /= speed;

            rb.linearVelocity *= newspeed;
        }
    }
    private void IsGrounded(){
        canJump = isGrounded = Physics.Raycast(transform.position,Vector3.down,height/2+0.2f,ground);
    }
    private void OnWall(){
        if(Physics.Raycast(transform.position,wishdir,1f,ground) && !isGrounded){
        
            Vector3 vel = rb.linearVelocity;
            vel.y+=Physics.gravity.y/1500;
            rb.linearVelocity = vel;

            rb.useGravity = false;
            IsOnWall = true;
            //canJump = true;
        }else{
            rb.useGravity = true;
            IsOnWall = false;
        }
    }
    private bool OnSlope(){
        if(Physics.Raycast(transform.position,Vector3.down,out slopeHit, height/2+0.5f)){
            if(slopeHit.normal != Vector3.up){
                return true;
            }else{
                return false;
            }
        }
        return false;
    }
    void stepClimb(){
        RaycastHit lowerHit;
        if(Physics.Raycast(stepArrayDown.transform.position,wishdir,out lowerHit,.6f)){
            RaycastHit upperHit;
            if(!Physics.Raycast(stepArrayUp.transform.position,wishdir,out upperHit,.6f)){
               rb.position -= new Vector3(0f,stepSmooth,0f); 
            }
        }
    }
    private void JumpInput(){
        jumpBufferCounter = jumpBufferTime;
    }
    private void IsJump(){
        jumpBufferCounter -= Time.deltaTime; // Decrease buffer over time
        if(canJump && jumpBufferCounter >= 0f){

            jumpBufferCounter = jumpBufferTime;

            //if(IsOnWall){
            //    rb.linearVelocity += new Vector3(jumpForce*2.5f*-wishdir.x,jumpForce*1.5f,0);
            //}else{
                rb.linearVelocity += new Vector3(rb.linearVelocity.x,jumpForce,rb.linearVelocity.z);
            //}
        
            jumpBufferCounter = 0f; // Reset buffer after jump
        }
    }
    private void FixedUpdate()
    {
        Move();
        Friction();
        stepClimb();
    }
    private void Update()
    {   
        MaxGroundSpeed = input.isRunning ? GroundRunSpeed : GroundRunSpeed-RunSpeed;
        MaxAirSpeed = input.isRunning ? AirRunSpeed : AirRunSpeed-RunSpeed;
        accel = input.isRunning ? AccelRun : AccelRun-1;

        OnSlope();
        IsGrounded();        
        OnWall();
        IsJump();

        PlaySound(); 

        //Debug.Log($"{input.x} : {input.y}");
        Speed = rb.linearVelocity.magnitude;
    }
    public float stepInterval = 0.125f;
    private float stepTimer; 
    bool wasGrounded = false;
    bool wasOnWall = false;
    void PlaySound(){

        if (!wasGrounded && isGrounded) 
        {
            PlayFootstep();
            stepTimer =0f; 
        }
        if (!wasOnWall && IsOnWall) 
        {
            PlayFootstep();
            stepTimer =0f; 
        }


        if(isGrounded && wishdir == Vector3.zero){
        //idle
        }else if(isGrounded || IsOnWall){
        stepTimer +=  IsOnWall ? Time.deltaTime*3 : Time.deltaTime ;
            if (stepTimer >= stepInterval)
            {
                PlayFootstep();
                stepTimer = 0f;
            }
        }else{
        //moveair
        }
        wasGrounded = isGrounded;
        wasOnWall = IsOnWall;
    }
    
    private int stepIndex = 0; // Tracks the last played footstep sound

void PlayFootstep()
{
    // Ensure the index alternates between 0 and 1
    audioSource.PlayOneShot(footSteps[stepIndex]);
    stepIndex = 1 - stepIndex; // Toggle between 0 and 1
}
    public void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position,1);
    }
}
    
