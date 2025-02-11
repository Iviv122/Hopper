using UnityEngine;

public class PlayerInfo : Entity 
{
    [SerializeField] public float IntervalBetweenDamage;
    [SerializeField] public int DamageInInterval;
    public void Start(){
        Health = MaxHealth;
        InvokeRepeating(nameof(ApplySelfDamage),0,IntervalBetweenDamage); 
    }
    public void ApplySelfDamage(){
        GetDamage(DamageInInterval);
    }

    public override void Die()
    {
        //Debug.Log("Now i am dead, but i will return >:3");
    }
}
