using System.Collections;
using UnityEngine;

abstract public class HomingProjectile : Projectile
{
    protected Rigidbody rb;
    public Transform target;
    public float MoveSpeed;
    public override void Spawn(Transform target){
        this.target = target;
    }
    protected virtual void FixedUpdate() {
        rb.linearVelocity = (target.position-transform.position).normalized * MoveSpeed;
        RotateProjectile();
    }
    protected virtual void OnTriggerEnter(Collider col){
        // here
    }
    protected virtual void OnTriggerStay(Collider col){
        //here
    }
    protected void Awake(){

        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }
    private void RotateProjectile(){
        Vector3 dir = target.position-transform.position;
        Quaternion rotation = Quaternion.LookRotation(dir,target.forward);
        transform.rotation = rotation;
        rb.MoveRotation(rotation);
    }

}