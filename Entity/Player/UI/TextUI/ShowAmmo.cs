using TMPro;
using UnityEngine;

public class ShowAmmo : MonoBehaviour
{
    [SerializeField] AmmoManager ammo;
    [SerializeField] WeaponManager weapon;
    [SerializeField] TextMeshProUGUI textField;
    Weapon CurrentWeapon;
    void Awake(){
        ammo.AmmoChanged += UpdateAmmo;
        weapon.weaponChanged += WeaponChanged;
    }
    void WeaponChanged(Weapon curWeapon){
        if(curWeapon.ammoType == AmmoType.None){
            textField.text = "";
            CurrentWeapon = curWeapon;
            return;
        }
        textField.text = ammo.CheckAmmoAmount(curWeapon.ammoType).ToString();
        CurrentWeapon = curWeapon;
    }
    void UpdateAmmo(){
        if(CurrentWeapon.ammoType == AmmoType.None){
            return;
        }
    textField.text = ammo.CheckAmmoAmount(CurrentWeapon.ammoType).ToString();
    }
}
