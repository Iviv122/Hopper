using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    void onAwake()
    {
       rb.useGravity = false; 
    }
    private void Start() {
        rb.AddForce(transform.forward*1f,ForceMode.Impulse);
    }
    void OnCollisionEnter(Collision other) {


        if(!other.gameObject.CompareTag(gameObject.tag)){
            Collider[] overlappedColliders = Physics.OverlapSphere(transform.position,1.5f);

            foreach (Collider item in overlappedColliders)
            {
                Rigidbody rigidbody = item.attachedRigidbody;
                if(rigidbody){
                    rigidbody.linearVelocity += (rigidbody.gameObject.transform.position-transform.position).normalized*10;
                }    
            }

            Debug.Log("Hit");
            Destroy(gameObject);
        }

    }
}
