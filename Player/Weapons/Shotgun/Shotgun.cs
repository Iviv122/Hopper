using System;
using UnityEditor.Callbacks;
using UnityEngine;

public class Shotgun : Weapon
{

    public override int MaxAmmo => 2;
    public override GameObject Bullet => Resources.Load("Projectiles/Pellet") as GameObject;
    private bool canShoot = true;
    void Awake()
    {
        CurrentAmmo = MaxAmmo;
        canShoot = true;
    }
    public override void Shoot(Camera cam, Rigidbody player)
    {
        if (canShoot && CurrentAmmo >0){
            Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 1f));
            Instantiate(Bullet, rayOrigin, cam.transform.rotation);

            player.AddForce(-cam.gameObject.transform.forward*10f,ForceMode.Impulse);

            CurrentAmmo--;
            canShoot = false;
            Invoke(nameof(ResetShot),0.25f);
        }
    }
    public void ResetShot(){
        canShoot=true;
    }
}
