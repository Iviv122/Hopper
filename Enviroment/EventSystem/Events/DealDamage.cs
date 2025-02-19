using UnityEngine;

public class DealDamage : IAction 
{
    [SerializeField] Entity[] entities;
    [SerializeField] int DamageAmount;

    public override void Action()
    {
        foreach (Entity i in entities)
        {
            if(i is PlayerInfo info){
            info.GetDamageNoIFrames(DamageAmount);
            }else{
            i.GetDamage(DamageAmount);
            }
        }
    }
}
