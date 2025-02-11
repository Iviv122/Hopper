using Unity.VisualScripting;
using UnityEngine;

public class HomingRocket : HomingProjectile 
{
    Vector3 Direction;
    void Destroy()
    {
        Destroy(gameObject);
    }
    
    void Explode()
    {
        Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, 3.5f);

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
    protected override void OnTriggerStay(Collider other)
    {
        Explode();
    }
    protected override void OnTriggerEnter(Collider other)
    {
        Explode();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}