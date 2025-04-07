using Unity.VisualScripting;
using UnityEngine;

public class RedirectiveJumpPad : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip audioSound;
    [SerializeField] Vector3 JumpVector;
    [SerializeField] float JumpPower;
    private void OnCollisionEnter(Collision other) {

        Rigidbody rb;
        if(other.gameObject.TryGetComponent<Rigidbody>(out rb)){
            rb.linearVelocity = JumpVector*JumpPower;
            audioSource.PlayOneShot(audioSound);
        }
        if(rb =other.gameObject.GetComponentInParent<Rigidbody>()){
            rb.linearVelocity = JumpVector*JumpPower;
            audioSource.PlayOneShot(audioSound);
        }
    }
    private void OnTriggerEnter(Collider other) {

        Rigidbody rb;
        if(other.gameObject.TryGetComponent<Rigidbody>(out rb)){
            rb.linearVelocity = JumpVector*JumpPower;
            audioSource.PlayOneShot(audioSound);
        }
        if(rb =other.gameObject.GetComponentInParent<Rigidbody>()){
            rb.linearVelocity = JumpVector*JumpPower;
            audioSource.PlayOneShot(audioSound);
        }
    }
    private void OnDrawGizmosSelected() {
    Gizmos.DrawLine(transform.position,JumpVector+transform.position);     
    }
}
