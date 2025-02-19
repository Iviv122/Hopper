using UnityEngine;

public class EmptyHands : Weapon
{
    public override void Shoot(AmmoManager ammo)
    {
        // TODO: ACTUALLY DO THIS WEAPON
        Debug.Log("Punch");
    }
}
