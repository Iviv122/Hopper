using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] PlayerMovement player;
    [SerializeField] InputManager input;
    [SerializeField] List<Weapon> weapons;
    [SerializeField] AmmoManager ammo;
    [SerializeField] Weapon currentWeapon;
    public event Action<Weapon> weaponChanged;
    private void Awake() {
        input.Shoot += Shoot;
        input.InputNumber += ChangeWeapon;
         

        EmptyHands hands = gameObject.GetComponent<EmptyHands>(); // or rather kick XDD
        hands.Initialize(cam,player.rb);
        currentWeapon = hands;
        weapons.Add(hands);
    
        weaponChanged?.Invoke(currentWeapon);
    }
    public void AddRocketLauncher(){
        if(GetComponent<RocketLauncher>() != null){
            return;
        }
        RocketLauncher rocketlauncher = gameObject.AddComponent<RocketLauncher>();
        rocketlauncher.Initialize(cam,player.rb);
        weapons.Add(rocketlauncher);
        currentWeapon = rocketlauncher;
        weaponChanged?.Invoke(currentWeapon);
    }
    public void AddShotgun(){
        if(GetComponent<Shotgun>() != null){
            return;
        }
        Shotgun shotgun = gameObject.AddComponent<Shotgun>();
        shotgun.Initialize(cam,player.rb);
        weapons.Add(shotgun);
        currentWeapon = shotgun;
        weaponChanged?.Invoke(currentWeapon);
    }
    public void MinigunAdd(){
        if(GetComponent<Minigun>() != null){
            return;
        }
        Minigun minigun = gameObject.AddComponent<Minigun>();
        minigun.Initialize(cam,player.rb);
        weapons.Add(minigun);
        currentWeapon = minigun;
        weaponChanged?.Invoke(currentWeapon);
    }
    public void Shoot(){
        if(currentWeapon.ammoType == AmmoType.None){
            currentWeapon.Shoot(ammo);
            return;
        }
        if(ammo.CheckAmmoAmount(currentWeapon.ammoType) >= currentWeapon.ammoConsumption){
            currentWeapon.Shoot(ammo);
        }
    }
    private Predicate<Weapon> isShotgun = (Weapon p) => { return p is Shotgun;};
    private Predicate<Weapon> isRocketLauncher = (Weapon p) => { return p is RocketLauncher;};
    private Predicate<Weapon> isMinigun = (Weapon p) => { return p is Minigun;};
    private Predicate<Weapon> isEmptyHands = (Weapon p) => { return p is EmptyHands;};

    public bool AnyWeapon(){
        return weapons.Count > 1;
    }
    public void ChangeWeapon(int index){
        currentWeapon = index switch
        {
            1 => weapons.Find(isEmptyHands) ?? currentWeapon,
            2 => weapons.Find(isShotgun) ?? currentWeapon,
            3 => weapons.Find(isRocketLauncher) ?? currentWeapon,
            4 => weapons.Find(isMinigun) ?? currentWeapon,
            _ => currentWeapon,
        };
        weaponChanged?.Invoke(currentWeapon);
    }
}
public enum WeaponType
{
    None, //0
    RocketLauncher, // 2
    ShotGun, // 1
    NailGun // 3
}
