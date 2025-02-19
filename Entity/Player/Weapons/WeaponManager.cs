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
    private void Awake() {
        input.Shoot += Shoot;
        input.InputNumber += changeWeapon;
    
        EmptyHands hands = gameObject.AddComponent<EmptyHands>();

    }
    public void AddRocketLauncher(){
        if(GetComponent<RocketLauncher>() != null){
            return;
        }
        RocketLauncher rocketlauncher = gameObject.AddComponent<RocketLauncher>();
        rocketlauncher.Initialize(cam,player.rb);
        weapons.Add(rocketlauncher);
    }
    public void AddShotgun(){
        if(GetComponent<Shotgun>() != null){
            return;
        }
        Shotgun shotgun = gameObject.AddComponent<Shotgun>();
        shotgun.Initialize(cam,player.rb);
        weapons.Add(shotgun);
    }
    public void MinigunAdd(){
        if(GetComponent<Minigun>() != null){
            return;
        }
        Minigun minigun = gameObject.AddComponent<Minigun>();
        minigun.Initialize(cam,player.rb);
        weapons.Add(minigun);
    }
    public void Shoot(){
        if(weapons.Count > 0 && ammo.CheckAmmoAmount(currentWeapon.ammoType) >= currentWeapon.ammoConsumption){
            currentWeapon.Shoot(ammo);
        }
    }
    private Predicate<Weapon> isShotgun = (Weapon p) => { return p is Shotgun;};
    private Predicate<Weapon> isRocketLauncher = (Weapon p) => { return p is RocketLauncher;};
    private Predicate<Weapon> isMinigun = (Weapon p) => { return p is Minigun;};
    private Predicate<Weapon> isEmptyHands = (Weapon p) => { return p is EmptyHands;};
    public void changeWeapon(int index){
        if(weapons.Count-1 < index){
            return;
        }
        if(index<0){
            index = 0;
        }
        currentWeapon = index switch
        {
            1 => weapons.Find(isShotgun),
            2 => weapons.Find(isRocketLauncher),
            3 => weapons.Find(isShotgun),
            _ => currentWeapon,
        };
    }

}
public enum WeaponType
{
    RocketLauncher, // 2
    ShotGun, // 1
    NailGun // 3
}
