using System;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{

    [Header("Dependecies")]
    [SerializeField] InputManager input;
    [SerializeField] PlayerMovement movement;
    public Transform orientation;
    [Header("Angles")]
    [SerializeField] public float tiltAngle; 
    [SerializeField] public float OnWallTilt;
    [Header("Sensitivity")] 
    public float sensX;
    public float sensY;

    float rotationX;
    float rotationY;

    void Awake(){
        sensX = PlayerSettings.SensX;
        sensY = PlayerSettings.SensX;

        PlayerSettings.updateValues += OnSensitivityUpdated;
    }

    private void OnSensitivityUpdated()
    {
        sensX = PlayerSettings.SensX;
        sensY = PlayerSettings.SensX;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {

        float mouseX = Input.GetAxisRaw("Mouse X") * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensY;

        float inputValue;
        if(movement.IsOnWall){
            inputValue= input.x * OnWallTilt;
        }else{
            inputValue= -input.x * tiltAngle;
        }
        rotationY += mouseX;
        rotationX -= mouseY;
    
        rotationX = Mathf.Clamp(rotationX, -90f, 90f); 

        transform.rotation =Quaternion.Slerp(transform.rotation,Quaternion.Euler(rotationX,rotationY,inputValue),15f*Time.deltaTime);
        orientation.rotation = Quaternion.Euler(0,rotationY,0);
    }
}
