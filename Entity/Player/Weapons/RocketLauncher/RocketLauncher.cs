using UnityEngine;
using UnityEngine.Pool;

public class RocketLauncher : Weapon
{
    private bool canShoot = true;
    private ObjectPool<Projectile> RocketPool;
    private AudioClip shootSound;
    private AudioSource soundSource;
    void Awake()
    {
        soundSource = gameObject.GetComponent<AudioSource>();

        canShoot = true;

        Bullet = Resources.Load("Projectiles/Rocket") as GameObject;
        shootSound = Resources.Load("Sounds/wpn_fire_rpg2") as AudioClip;

        ammoType = AmmoType.Rocket;
        ammoConsumption = 1;
        
        RocketPool = new ObjectPool<Projectile>(CreatePooledObject,OnTakeFromPool,OnReturnToPool, OnDestroyObject,false,100,1000);
    }
    private void ReturnObjectToPool(Projectile Instance){
        RocketPool.Release(Instance);
    }
    private Projectile CreatePooledObject(){
        Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
        
        Projectile Instance = Instantiate(Bullet, rayOrigin, cam.transform.rotation).GetComponent<Projectile>();
        Instance.Disable += (Projectile p) => ReturnObjectToPool(p as Rocket);
        Instance.Spawn(transform);
        Instance.gameObject.SetActive(true);

        return Instance; 
    }
    private void OnTakeFromPool(Projectile Instance){
        Instance.gameObject.SetActive(true);
        Instance.Spawn(transform);
        SpawnBullet(Instance);
    }
    private void OnReturnToPool(Projectile Instance){
        Instance.gameObject.SetActive(false);
    }
    private void OnDestroyObject(Projectile Instance){
        Destroy(Instance);
    }
    public void SpawnBullet(Projectile Instance){
        Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));

        Instance.transform.position = rayOrigin;
        Instance.transform.rotation = cam.transform.rotation;
    }
    public override void Shoot(AmmoManager ammo)
    {
        if (canShoot){
                Debug.Log("Actually shot");
            Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));

            soundSource.PlayOneShot(shootSound);
            RocketPool.Get();
            ammo.RemoveAmmo(ammoType,ammoConsumption);

            canShoot=false;
            Invoke(nameof(ResetShot),0.8f);
        }
    }

    public void ResetShot(){
        canShoot=true;
    }
}
