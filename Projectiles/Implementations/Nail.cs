// Is it Quake reference?
using UnityEngine;

public class Nail : Projectile 
{
    [SerializeField] Rigidbody rb;

    public void Awake(){

        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    
        Invoke(nameof(Destroy),3);
    }
    public override void Spawn(Transform target)
    {
        //placeholder
    }
    void Destroy(){
        Destroy(gameObject);
    }
    void Update()
    {
        rb.linearVelocity=transform.forward*60f;
    }
    void OnTriggerEnter(Collider other){
        if(!other.gameObject.CompareTag(gameObject.tag)){
            Entity target = other.gameObject.GetComponent<Entity>();
            if(!target){
            target = other.gameObject.GetComponentInParent<Entity>();
            }
            if(target){
                target.GetDamage(2);
            }
        }
        Destroy(gameObject);
    }
}
