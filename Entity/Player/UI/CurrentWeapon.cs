using System;
using System.Collections.Generic;
using UnityEngine;

public class CurrentWeapon : MonoBehaviour
{
    [SerializeField] WeaponManager weapons;
    [SerializeField] GameObject[] weaponsModels;

    private void Awake() {
        foreach (GameObject item in weaponsModels)
        {
           item.SetActive(false); 
        }
        weapons.weaponChanged +=HandleUpdate;  
        
    }
    void HandleUpdate(Weapon CurrentWeapon){
        switch (CurrentWeapon)
        {
            case EmptyHands:
                ChangeModel(0);
            break;
            case Shotgun:
                ChangeModel(1);
            break;
            case RocketLauncher:
                ChangeModel(2);
            break;           
            case Minigun:
                ChangeModel(3);
            break; 
            default:
                break;
        }
    }
    void ChangeModel(int index){
        foreach (GameObject item in weaponsModels)
        {
           item.SetActive(false); 
        }
        weaponsModels[index].SetActive(true);
    }
}
