using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Rendering;

public class Rocket : Projectile 
{
    [SerializeField] Rigidbody rb;
    Vector3 Direction;
    void Awake(){
        rb = GetComponent<Rigidbody>(); 
        rb.useGravity = false;
        Invoke(nameof(Destroy), 3);
    }
    public override void Spawn(Transform target)
    {
        // no need i guess XD
    }
    void Destroy()
    {
        Destroy(gameObject);
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = transform.forward * 100f;
        //if (Physics.CheckBox(transform.position, transform.localScale/2, transform.rotation, ignore))
        //{
        //    Explode();
        //}
    }

    void Explode()
    {
        Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, 3.5f);
        DestroyBreakable(transform,3.5f,20); 
        
        foreach (Collider item in overlappedColliders)
        {
            Rigidbody rigidbody = item.GetComponent<Rigidbody>();
            if (!rigidbody)
            {
                rigidbody = item.GetComponentInParent<Rigidbody>();
            }
            if (rigidbody)
            {
                if (!rigidbody.isKinematic)
                {
                    float Length = (rigidbody.gameObject.transform.position - transform.position).magnitude;
                    Direction = (rigidbody.gameObject.transform.position - transform.position).normalized;
                    if (Length < 3.25)
                    {
                        Direction = -Direction;
                    }
                    if (rigidbody.name == "Player")
                    {
                        Debug.Log("Rocket n Player Distance " + Length);
                    }

                    rigidbody.linearVelocity += Direction * 20;
                    //rigidbody.AddExplosionForce(20f,transform.position,3.5f,1,ForceMode.VelocityChange);
                }
            }
            Entity entity = item.GetComponent<Entity>();
            if (!entity)
            {
                entity = item.GetComponentInParent<Entity>();
            }
            if (entity)
            {
                entity.GetDamage(20);
            }
        }

        Destroy(gameObject);


    }
    void OnTriggerStay(Collider other)
    {
        Explode();
    }
}