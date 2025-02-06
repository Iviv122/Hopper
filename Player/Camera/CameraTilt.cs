using UnityEngine;

public class CameraTilt : MonoBehaviour
{
   [SerializeField] InputManager input;
   [SerializeField] float tiltAngle; 

    void Update()
    {
        float inputValue = input.x * tiltAngle;
 
        Vector3 upOfParent = Quaternion.Inverse(transform.localRotation) * transform.up;

        Vector3 upDir = Quaternion.AngleAxis(-inputValue, transform.forward) * upOfParent;

        Quaternion goalRot = Quaternion.LookRotation(transform.forward, upDir);

        transform.rotation = Quaternion.Slerp(transform.rotation, goalRot, 5f * Time.deltaTime); 
    }
}
