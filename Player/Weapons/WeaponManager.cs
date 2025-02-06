using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] PlayerMovement player;
    [SerializeField] InputManager input;
    [SerializeField] List<Weapon> weapons;
    [SerializeField] int currentWeapon=0;
    private void Awake() {
        player.groundTouched += OnGroundTouch;
        input.Shoot += Shoot;
        input.InputNumber += changeWeapon;
    }
    public void OnGroundTouch(){
        foreach(Weapon i in weapons){
            i.CurrentAmmo = i.MaxAmmo;
        } 
    }
    public void AddRocketLauncher(){
        RocketLauncher rocketlauncher = gameObject.AddComponent<RocketLauncher>();
        weapons.Add(rocketlauncher);
    }
    public void AddShotgun(){
        Shotgun shotgun = gameObject.AddComponent<Shotgun>();
        weapons.Add(shotgun);
    }
    public void Shoot(){
        try{
            weapons[currentWeapon].Shoot(cam,player.rb);
        }catch(IndexOutOfRangeException e){
            Debug.Log(e);
        }
    }
    public void changeWeapon(int index){
        index--;
        if(weapons.Count-1 < index){
            return;
        }
        if(index<0){
            index = 0;
        }
        currentWeapon = index;
    }
}
