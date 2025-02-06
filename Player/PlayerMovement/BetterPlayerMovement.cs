using System;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class OldPlayerMovement : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] InputManager input;
    [SerializeField] Transform orientation;
    [Header("Speed")]
    [SerializeField] float moveSpeed;
    [SerializeField] float walkSpeed; // should be less
    [SerializeField] float slideSpeed; // slightlty bigger speed, we can`t rotate in such state
    [SerializeField] float stopSpeed;
    [SerializeField] float flySpeed;
    [Header("Jump")]
    [SerializeField] float jumpPower;
    [SerializeField] public bool isGrounded;
    [SerializeField] LayerMask groundLayer;
    [Header("Slide/Crouch")]
    [SerializeField] Vector3 normalScale = new Vector3(1,1f,1);
    [SerializeField] Vector3 crouchScale = new Vector3(1,0.5f,1);
    [SerializeField] float height = 2f;
    [Header("Resistanse")]
    [SerializeField] float friction;
    [SerializeField] float groundFriction = 3f;
    public Rigidbody rb;
    public Vector3 moveDir, lastMoveDir;
    [SerializeField]    private MoveState state;
    public event Action groundTouched; 
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        input.CrouchStart += StartCrouch;
    }
    void FixedUpdate(){
        Move();
        Jump();
        Friction();
    }
    void HandleState(){
        if(input.isCrouching && isGrounded){
            state = MoveState.Slide;
            moveSpeed = slideSpeed;
            height = 1f;
            transform.localScale = crouchScale;
            moveDir = lastMoveDir;
            friction = groundFriction;
        }else if(isGrounded){
            state = MoveState.Walk;
            moveSpeed = walkSpeed;
            height = 2f;
            transform.localScale = normalScale;
            friction = groundFriction;
        }else if(!isGrounded){
            state = MoveState.Fly;
            moveSpeed = flySpeed;
            height = 1.9f;
            transform.localScale = normalScale;
        }
    }
    void Move(){
        switch (state)
        {
            case MoveState.Slide:
                moveDir = lastMoveDir;
                break;
            default:
                moveDir = (input.x * orientation.right + input.y * orientation.forward).normalized;
                if(moveDir != Vector3.zero){
                    lastMoveDir = moveDir;
                }else{
                    lastMoveDir = rb.linearVelocity.normalized;
                    lastMoveDir.y=0;
                }
                break;
        }
        rb.linearVelocity += moveDir*moveSpeed*Time.deltaTime;
    }
    void Jump(){
        if(input.isJumping && isGrounded){
            rb.linearVelocity = new Vector3(rb.linearVelocity.x,jumpPower,rb.linearVelocity.z);
        }
    }
    void Friction(){
        if(isGrounded){
            float speed = Mathf.Sqrt(rb.linearVelocity.x*rb.linearVelocity.x+rb.linearVelocity.z*rb.linearVelocity.z);
            if(speed < 1){
                rb.linearVelocity = new Vector3(0,rb.linearVelocity.y,0);
                return ;
            }
            float control = speed < stopSpeed ? stopSpeed : speed; 



            float drop =0;
            drop += control*friction*Time.deltaTime;

            float newspeed = speed - drop;
            if(newspeed < 0){
                newspeed = 0;
            }
            newspeed /= speed;
            rb.linearVelocity *= newspeed; 
        }
    }
    void Grounded(){
        switch(state){
            case MoveState.Slide:
                isGrounded = Physics.Raycast(transform.position,-transform.up,height*2+0.1f,groundLayer);
            break; 
            default:
               isGrounded = Physics.Raycast(transform.position,-transform.up,(height/2)+0.1f,groundLayer); 
            break;
        }
    }
    void StartCrouch(){
            lastMoveDir = moveDir; 
    }
    void SaveDir(){
        lastMoveDir = moveDir;
    }
    void Update()
    {
        StartCrouch();

        Grounded();
        HandleState();
        if(isGrounded){
            groundTouched?.Invoke();
        }
    }
}
public enum MoveState{
    Walk,
    Fly,
    Slide,
    WallRun
}