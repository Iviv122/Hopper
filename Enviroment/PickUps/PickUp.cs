using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UIElements;

public class PickUp : MonoBehaviour
{
    public PickUpType pickUpType;
    [Header("Weapon")]
    public WeaponType weapon;
    [Header("Health")]
    public int healthAmmount;
    [Header("Ammo")]
    public int AmmoAmount;
    public AmmoType ammoType;
    [Header("Armour")]
    public int ArmourAmmount;
    private void Pick(GameObject player){

        switch (pickUpType)
        {
            case PickUpType.Weapon:
                AddItem(player.GetComponentInParent<WeaponManager>());
                AddAmmo(player.GetComponentInParent<AmmoManager>(),weapon);
            break;
            case PickUpType.Health:
                player.GetComponentInParent<PlayerInfo>().Health+= healthAmmount;
            break;
            case PickUpType.Ammo:
                AddAmmo(player.GetComponentInParent<AmmoManager>()); 
            break;
            case PickUpType.Armour:
                player.GetComponentInParent<PlayerInfo>().Armour += ArmourAmmount;
            break;
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Pick(col.gameObject);
            Destroy(gameObject);
    
        }
    }
    void AddItem(WeaponManager player)
    {
        switch (weapon)
        {
            case WeaponType.RocketLauncher:
                player.AddRocketLauncher();
                return;
            case WeaponType.ShotGun:
                player.AddShotgun();
                return;
            case WeaponType.NailGun:
                player.MinigunAdd();
                return;

        }
    }
    void AddAmmo(AmmoManager player){
        switch(ammoType){
            case AmmoType.Nails:
                player.Nails += AmmoAmount;
            return;
            case AmmoType.Rocket:
                player.Rockets += AmmoAmount;
            return;
            case AmmoType.BuckShot:
                player.BuckShots += AmmoAmount;
            return;
        }
    }
    void AddAmmo(AmmoManager player, WeaponType weapon){
        switch(weapon){
            case WeaponType.NailGun:
                player.Nails += 100;
            return;
            case WeaponType.RocketLauncher:
                player.Rockets += 10;
            return;
            case WeaponType.ShotGun:
                player.BuckShots += 15;
            return;
        }
    }
}
public enum PickUpType{
    Weapon,
    Ammo,
    Health,
    Armour
}


