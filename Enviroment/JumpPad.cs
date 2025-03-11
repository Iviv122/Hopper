using Unity.VisualScripting;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] Vector3 JumpVector;
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rb)){
            rb.linearVelocity += JumpVector;
        }
    }
}
