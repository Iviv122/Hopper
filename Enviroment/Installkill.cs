using UnityEngine;

public class Installkill : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {

        Rigidbody rb;
        if(rb =other.gameObject.GetComponentInParent<Rigidbody>()){
            rb.GetComponent<PlayerInfo>().Die();
        }
    }
    private void OnTriggerEnter(Collider other) {

        Rigidbody rb;
        if(rb =other.gameObject.GetComponentInParent<Rigidbody>()){
            rb.GetComponent<PlayerInfo>().Die();
        }
    }        
}
