using UnityEngine;

public class Pellet : MonoBehaviour
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
            Debug.Log("Hit");
            Destroy(gameObject);
        }

    }
}
