using UnityEngine;

public class CameraTilt : MonoBehaviour
{
    /*
    [SerializeField] InputManager input;
    
    void Awake(){

    }

    void Update()
    {
        if(movement.IsOnWall){
            CurrentTilt = OnWallTilt * -movement.wishdir.x;
        }else{
            CurrentTilt = tiltAngle;
        }

        float inputValue = input.x * tiltAngle;
 
        Vector3 upOfParent = Quaternion.Inverse(transform.localRotation) * transform.up;

        Vector3 upDir = Quaternion.AngleAxis(-inputValue, transform.forward) * upOfParent;

        Quaternion goalRot = Quaternion.LookRotation(transform.forward, upDir);

        transform.rotation = Quaternion.Slerp(transform.rotation, goalRot, 5f * Time.deltaTime); 
    }
    */
}
