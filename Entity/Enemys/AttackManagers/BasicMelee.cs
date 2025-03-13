using UnityEngine;

public class BasicMelee : AttackManager 
{
    public int Damage;
    public override void AttackMelee(Transform target)
    {
        Entity victim = target.GetComponent<Entity>() ?? target.GetComponentInParent<Entity>();

        victim.GetDamage(Damage);
    }
}
