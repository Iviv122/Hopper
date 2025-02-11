using UnityEngine;

public class Pellet : Projectile 
{
    [SerializeField] Rigidbody rb;
    
    public override void Spawn(Transform target){

        rb.useGravity = false;
        Invoke(nameof(Destroy),3);
    }
    void Destroy(){
        Destroy(gameObject);
    }
    private void Update() {
        rb.linearVelocity = transform.forward*200f;
    }
    void OnTriggerEnter(Collider other) {

        if(!other.gameObject.CompareTag(gameObject.tag)){
            Entity target = other.gameObject.GetComponent<Entity>();
            if(!target){
                target = other.gameObject.GetComponentInParent<Entity>();
            }
            if(target){
                target.GetDamage(5);
            }
            Destroy(gameObject);
        }

    }
}
