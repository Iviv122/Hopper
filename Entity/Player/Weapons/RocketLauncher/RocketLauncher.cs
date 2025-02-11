using UnityEngine;
using UnityEngine.Pool;

public class RocketLauncher : Weapon
{
    public override int MaxAmmo => 4;
    private bool canShoot = true;
    private ObjectPool<Projectile> RocketPool;
    void Awake()
    {
        CurrentAmmo = MaxAmmo;
        canShoot = true;
        Bullet = Resources.Load("Projectiles/Rocket") as GameObject;
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
    public override void Shoot()
    {
        if (canShoot && CurrentAmmo >0){
                Debug.Log("Actually shot");
            Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));

            RocketPool.Get();

            canShoot=false;
            CurrentAmmo--;
            Invoke(nameof(ResetShot),0.8f);
        }
    }

    public void ResetShot(){
        canShoot=true;
    }
}
