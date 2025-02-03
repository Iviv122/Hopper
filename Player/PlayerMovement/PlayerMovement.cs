using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    //Assingables
    public Transform orientation;
    public InputManager input;
    
    //Other
    public Rigidbody rb;
    //Movement
    public float moveSpeed = 4500;
    public float maxSpeed = 20;
    public bool grounded;
    public LayerMask whatIsGround;
    
    public float counterMovement = 0.175f;
    private float threshold = 0.01f;
    public float maxSlopeAngle = 35f;

    //Crouch & Slide
    private Vector3 crouchScale = new Vector3(1, 0.5f, 1);
    private Vector3 playerScale;

    //Jumping
    private bool readyToJump = true;
    private float jumpCooldown = 0.25f;
    public float jumpForce = 550f;
    
    //Sliding
    private Vector3 normalVector = Vector3.up;
    private Vector3 wallNormalVector;

    public event Action groundTouched;

    void Awake() {
        rb = GetComponent<Rigidbody>();
    }
    
    void Start() {
        playerScale =  transform.localScale;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void FixedUpdate() {
        Movement();
    }
    private void Update() {
        if(input.isCrouchingDown()){
            StartCrouch();
        }
        if(input.isCrouchingUp()){
            StopCrouch();
        }
    }

    private void StartCrouch() {
        transform.localScale = crouchScale;
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
    }

    private void StopCrouch() {
        transform.localScale = playerScale;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    }

    private void Movement() {
        //Extra gravity
        rb.AddForce(Vector3.down * Time.deltaTime * 10);
        
        //Find actual velocity relative to where player is looking
        Vector2 mag = FindVelRelativeToLook();
        float xMag = mag.x, yMag = mag.y;

        //Counteract sliding and sloppy movement
        CounterMovement(input.x, input.y, mag);
        
        //If holding jump && ready to jump, then jump
        if (readyToJump && input.isJumping()) Jump();

        //If sliding down a ramp, add force down so player stays grounded and also builds speed
        if (input.isCrouching() && grounded && readyToJump) {
            rb.AddForce(Vector3.down * Time.deltaTime * 3000);
        }
        
        //If speed is larger than maxspeed, cancel out the input so you don't go over max speed
        if (input.x > 0 && xMag > maxSpeed) input.x=0;
        if (input.x < 0 && xMag < -maxSpeed) input.x=0;
        if (input.y > 0 && yMag > maxSpeed) input.y=0;
        if (input.y < 0 && yMag < -maxSpeed) input.y=0;

        //Some multipliers
        float multiplier = 1f, multiplierV = 1f;
        
        // Movement in air
        if (!grounded) {
            multiplier = 0.5f;
            multiplierV = 0.5f;
        }
        
        // Movement while sliding
        if (grounded && input.isCrouching()){ 
            multiplierV = 1f;
        }

        //Apply forces to move player
        rb.AddForce(orientation.transform.forward * input.y * moveSpeed * Time.deltaTime * multiplier * multiplierV);
        rb.AddForce(orientation.transform.right * input.x * moveSpeed * Time.deltaTime * multiplier);
    }

    private void Jump() {
        if (grounded && readyToJump) {
            readyToJump = false;

            //Add jump forces
            rb.AddForce(Vector2.up * jumpForce * 1.5f);
            rb.AddForce(normalVector * jumpForce * 0.5f);
            Debug.Log("Jump"); 
            //If jumping while falling, reset y velocity.
            Vector3 vel = rb.linearVelocity;
            if (rb.linearVelocity.y < 0.5f)
                rb.linearVelocity = new Vector3(vel.x, 0, vel.z);
            else if (rb.linearVelocity.y > 0) 
                rb.linearVelocity = new Vector3(vel.x, vel.y / 2, vel.z);
            
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }
    
    private void ResetJump() {
        readyToJump = true;
    }
    
    private void CounterMovement(float x, float y, Vector2 mag) {
        if (!grounded || input.isJumping()) return;

        //Counter movement
        if (Math.Abs(mag.x) > threshold && Math.Abs(x) < 0.05f || (mag.x < -threshold && x > 0) || (mag.x > threshold && x < 0)) {
            rb.AddForce(moveSpeed * orientation.transform.right * Time.deltaTime * -mag.x * counterMovement);
        }
        if (Math.Abs(mag.y) > threshold && Math.Abs(y) < 0.05f || (mag.y < -threshold && y > 0) || (mag.y > threshold && y < 0)) {
            rb.AddForce(moveSpeed * orientation.transform.forward * Time.deltaTime * -mag.y * counterMovement);
        }
        
        //Limit diagonal running. This will also cause a full stop if sliding fast and un-crouching, so not optimal.
        if (Mathf.Sqrt(Mathf.Pow(rb.linearVelocity.x, 2) + Mathf.Pow(rb.linearVelocity.z, 2)) > maxSpeed) {
            float fallspeed = rb.linearVelocity.y;
            Vector3 n = rb.linearVelocity.normalized * maxSpeed;
            rb.linearVelocity = new Vector3(n.x, fallspeed, n.z);
        }
    }

    /// <summary>
    /// Find the velocity relative to where the player is looking
    /// Useful for vectors calculations regarding movement and limiting movement
    /// </summary>
    /// <returns></returns>
    public Vector2 FindVelRelativeToLook() {
        float lookAngle = orientation.transform.eulerAngles.y;
        float moveAngle = Mathf.Atan2(rb.linearVelocity.x, rb.linearVelocity.z) * Mathf.Rad2Deg;

        float u = Mathf.DeltaAngle(lookAngle, moveAngle);
        float v = 90 - u;

        float magnitue = rb.linearVelocity.magnitude;
        float yMag = magnitue * Mathf.Cos(u * Mathf.Deg2Rad);
        float xMag = magnitue * Mathf.Cos(v * Mathf.Deg2Rad);
        
        return new Vector2(xMag, yMag);
    }

    private bool IsFloor(Vector3 v) {
        float angle = Vector3.Angle(Vector3.up, v);
        return angle < maxSlopeAngle;
    }

    private bool cancellingGrounded;
    
    /// <summary>
    /// Handle ground detection
    /// </summary>
    private void OnCollisionStay(Collision other) {
        //Make sure we are only checking for walkable layers
        int layer = other.gameObject.layer;
        if (whatIsGround != (whatIsGround | (1 << layer))) return;

        //Iterate through every collision in a physics update
        for (int i = 0; i < other.contactCount; i++) {
            Vector3 normal = other.contacts[i].normal;
            //FLOOR
            if (IsFloor(normal)) {
                grounded = true;
                cancellingGrounded = false;
                normalVector = normal;
                CancelInvoke(nameof(StopGrounded));

                groundTouched?.Invoke();
            }
        }

        //Invoke ground/wall cancel, since we can't check normals with CollisionExit
        float delay = 3f;
        if (!cancellingGrounded) {
            cancellingGrounded = true;
            Invoke(nameof(StopGrounded), Time.deltaTime * delay);
        }
    }

    private void StopGrounded() {
        grounded = false;
    }
    
}