using System;
using UnityEngine;
using UnityEngine.XR;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputManager input;
    [SerializeField] private Transform orientation;
    [SerializeField] private float Speed;
    [SerializeField] private float MaxGroundSpeed = 25;
    [SerializeField] private float MaxAirSpeed = 25;
    [SerializeField] private float RunSpeed;
    [SerializeField] private float MoveSpeed = 15;
    [SerializeField] private float stopSpeed = 5; // static?
    [SerializeField] private float friction = 0.8f; // kinetic
    [SerializeField] private float accel = 3;
    [SerializeField] private bool isGrounded;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float height;
    [SerializeField] private Vector3 wishvel;
    [SerializeField] private Vector3 wishdir;
    [SerializeField] public Rigidbody rb;
    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [Header("Steps")]
    [SerializeField] private GameObject stepArrayUp;
    [SerializeField] private GameObject stepArrayDown;
    [SerializeField] private float stepSmooth;
    [SerializeField] RaycastHit slopeHit;
    public event Action groundTouched;

    private float GroundRunSpeed;
    private float AirRunSpeed;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        input.Jump += Jump;

        GroundRunSpeed = MaxGroundSpeed + RunSpeed;
        AirRunSpeed = MaxAirSpeed + RunSpeed;
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
    private void Jump(){
        if(isGrounded){
            rb.linearVelocity += new Vector3(rb.linearVelocity.x,jumpForce,rb.linearVelocity.z);
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
        isGrounded = Physics.Raycast(transform.position,Vector3.down,height/2+0.2f,ground);
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
        
        IsGrounded();
        
        //Debug.Log($"{input.x} : {input.y}");
        
    }
}