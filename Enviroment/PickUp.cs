using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PickUp : MonoBehaviour
{
    public PickUpType pickUpType;
    public WeaponType weapon;
    public int healthAmmount;

    private void Pick(GameObject player){

        switch (pickUpType)
        {
            case PickUpType.Weapon:
                AddItem(player.GetComponentInParent<WeaponManager>());
            break;
            case PickUpType.Health:
                player.GetComponentInParent<PlayerInfo>().Health+= healthAmmount;
            break;
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Pick(col.gameObject);
            Destroy(gameObject);
    
            Debug.Log("Weapon added");
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
}
public enum PickUpType{
    Weapon,
    Ammo,
    Health,
    // Armour?
}
public enum AmmoType{
    Nails,
    Rocket,
    BuckShot,
    // other
}
public enum WeaponType
{
    RocketLauncher,
    ShotGun,
    NailGun
}
