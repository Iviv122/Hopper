using UnityEngine;

public class TiltUI : MonoBehaviour
{
    [SerializeField] InputManager input;
    [SerializeField] float tiltAngle;

    void Update(){
    float inputValue = -input.x * tiltAngle;
    transform.localRotation =Quaternion.Slerp(transform.rotation,Quaternion.Euler(0,0,inputValue),15f*Time.deltaTime);
    }
}
