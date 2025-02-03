using UnityEngine;

public class RocketLauncher : Weapon
{
    public override GameObject Bullet => Resources.Load("Projectiles/Rocket") as GameObject;
    public override int MaxAmmo => 4;
    private bool canShoot = true;

    void Awake()
    {
        CurrentAmmo = MaxAmmo;
        canShoot = true;
    }

    public override void Shoot(Camera cam, Rigidbody player)
    {
        if (canShoot && CurrentAmmo >0){
                Debug.Log("Actually shot");
            Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 1f));
            Instantiate(Bullet, rayOrigin, cam.transform.rotation);

            canShoot=false;
            CurrentAmmo--;
            Invoke(nameof(ResetShot),0.8f);
        }
    }

    public void ResetShot(){
        canShoot=true;
    }
}
