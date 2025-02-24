using UnityEngine;

public class Pellet : Projectile 
{
    [SerializeField] Rigidbody rb;
    public void Awake(){
        if(rb == null){
            rb = GetComponent<Rigidbody>();
        }
        rb.useGravity = false;
        Invoke(nameof(Destroy),3);
    } 
    public override void Spawn(Transform target){
        if(rb == null){
            rb = GetComponent<Rigidbody>();
        }
        rb.useGravity = false;
        Invoke(nameof(Destroy),3);
    }
    void Destroy(){
        Destroy(gameObject);
    }
    private void Update() {
        rb.linearVelocity = transform.forward*200f;
    }
        
    private void OnCollisionEnter(Collision other) {
            Entity target = other.gameObject.GetComponent<Entity>();
            if(!target){
            target = other.gameObject.GetComponentInParent<Entity>();
            }
            if(target){
                target.GetDamage(1);
            }
        Destroy(gameObject);
    }
}
