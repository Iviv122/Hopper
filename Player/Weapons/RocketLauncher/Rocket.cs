using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    void onAwake()
    {
       rb.useGravity = false; 
    }
    private void Start() {
        rb.AddForce(transform.forward*100f,ForceMode.Impulse);
    }
    void OnCollisionEnter(Collision other) {


        if(!other.gameObject.CompareTag(gameObject.tag)){
            Collider[] overlappedColliders = Physics.OverlapSphere(transform.position,7.5f);

            foreach (Collider item in overlappedColliders)
            {
                Rigidbody rigidbody = item.attachedRigidbody;
                if(rigidbody){
                    rigidbody.AddExplosionForce(250f,transform.position,7.5f);
                }    
            }

            Debug.Log("Hit");
            Destroy(gameObject);
        }

    }
}
