using Unity.VisualScripting;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    public WeaponType weapon;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            AddItem(col.gameObject.GetComponentInParent<WeaponManager>());
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

        }
    }
}

public enum WeaponType
{
    RocketLauncher,
    ShotGun
}
