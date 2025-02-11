using UnityEngine;
using UnityEngine.Pool;

public class Shotgun : Weapon
{

    public override int MaxAmmo => 2;
    private bool canShoot = true;
    private ObjectPool<Pellet> PelletPool;
    void Awake()
    {
        CurrentAmmo = MaxAmmo;
        canShoot = true;
        Bullet = Resources.Load("Projectiles/Pellet") as GameObject;
        PelletPool = new ObjectPool<Pellet>(CreatePooledObject,OnTakeFromPool,OnReturnToPool, OnDestroyObject,false,200,2000);
    }
    private void ReturnObjectToPool(Pellet Instance){
        PelletPool.Release(Instance);
    }
    private Pellet CreatePooledObject(){
        Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 1f));
        
        Pellet Instance = Instantiate(Bullet, rayOrigin, cam.transform.rotation).GetComponent<Pellet>();
        Instance.Disable += (Projectile p) => ReturnObjectToPool(p as Pellet);
        Instance.Spawn(transform);
        Instance.gameObject.SetActive(true);

        return Instance; 
    }
    private void OnTakeFromPool(Pellet Instance){
        Instance.gameObject.SetActive(true);
        Instance.Spawn(transform);
        SpawnBullet(Instance);
    }
    private void OnReturnToPool(Pellet Instance){
        Instance.gameObject.SetActive(false);
    }
    private void OnDestroyObject(Pellet Instance){
        Destroy(Instance);
    }
    public void SpawnBullet(Pellet Instance){
        Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 1f));

        Instance.transform.position = rayOrigin;
        Instance.transform.rotation = cam.transform.rotation;
    }
    public override void Shoot()
    {
        if (canShoot && CurrentAmmo >0){
            Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 1f));

            rb.linearVelocity += -cam.transform.forward*25f;
            //Instnce
            PelletPool.Get();

            CurrentAmmo--;
            canShoot = false;
            Invoke(nameof(ResetShot),0.25f);
        }
    }
    public void ResetShot(){
        canShoot=true;
    }
}
