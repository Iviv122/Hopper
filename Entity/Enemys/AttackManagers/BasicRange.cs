using UnityEngine;

public class BasicRange : AttackManager 
{
    public GameObject Projectile;
    public Vector3 dir;
    public Vector3 SpawnOffset;
    public override void AttackRange(Transform target)
    {
        Vector3 dir = (target.position-transform.position).normalized;
        Projectile ins = Instantiate(Projectile,transform.position+SpawnOffset,Quaternion.LookRotation(dir,target.forward)).GetComponent<Projectile>();
        ins.Spawn(target);
    }

}
