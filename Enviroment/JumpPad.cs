using Unity.VisualScripting;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] Vector3 JumpVector;
    [SerializeField] float JumpPower;
    private void OnCollisionEnter(Collision other) {

        Rigidbody rb;
        if(other.gameObject.TryGetComponent<Rigidbody>(out rb)){
            rb.linearVelocity += JumpVector*JumpPower;
        }
        if(rb =other.gameObject.GetComponentInParent<Rigidbody>()){
            rb.linearVelocity += JumpVector*JumpPower;
        }
    }
    private void OnTriggerEnter(Collider other) {

        Rigidbody rb;
        if(other.gameObject.TryGetComponent<Rigidbody>(out rb)){
            rb.linearVelocity += JumpVector*JumpPower;
        }
        if(rb =other.gameObject.GetComponentInParent<Rigidbody>()){
            rb.linearVelocity += JumpVector*JumpPower;
        }
    }
    private void OnDrawGizmosSelected() {
    Gizmos.DrawLine(transform.position,JumpVector+transform.position);     
    }
}
