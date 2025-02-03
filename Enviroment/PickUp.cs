using Unity.VisualScripting;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    public WeaponType weapon;

    private void OnTriggerEnter(Collider col)
    {
        Debug.Log("Weapon added");
        if (col.gameObject.CompareTag("Player"))
        {
            AddItem(col.gameObject.GetComponent<WeaponManager>());
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

        }
    }
}

public enum WeaponType
{
    RocketLauncher,
    ShotGun
}
