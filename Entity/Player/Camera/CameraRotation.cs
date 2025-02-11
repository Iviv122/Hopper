using UnityEngine;

public class CameraRotation : MonoBehaviour
{

   [SerializeField] InputManager input;
   [SerializeField] float tiltAngle; 
    public float sensX;
    public float sensY;

    public Transform orientation;

    float rotationX;
    float rotationY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {

        float mouseX = Input.GetAxisRaw("Mouse X") * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensY;
        
        float inputValue = -input.x * tiltAngle;

        rotationY += mouseX;
        rotationX -= mouseY;
    
        rotationX = Mathf.Clamp(rotationX, -90f, 90f); 

        transform.rotation =Quaternion.Slerp(transform.rotation,Quaternion.Euler(rotationX,rotationY,inputValue),15f*Time.deltaTime);
        orientation.rotation = Quaternion.Euler(0,rotationY,0);
    }
}
