using UnityEngine;
using UnityEngine.Pool;

public class Minigun : Weapon 
{
    public override int MaxAmmo => -1;
    private bool canShoot = true;
    private ObjectPool<Nail> NailPool;
    void Awake()
    {
        canShoot = true;
        Bullet = Resources.Load("Projectiles/Nail") as GameObject;
        NailPool = new ObjectPool<Nail>(CreatePooledObject,OnTakeFromPool,OnReturnToPool, OnDestroyObject,false,200,100_000);
    }
    private void ReturnObjectToPool(Nail Instance){
        NailPool.Release(Instance);
    }
    private Nail CreatePooledObject(){
        Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 1f));
        
        Nail Instance = Instantiate(Bullet, rayOrigin, cam.transform.rotation).GetComponent<Nail>();
        Instance.Disable += (Projectile p) => ReturnObjectToPool(p as Nail);
        Instance.gameObject.SetActive(true);

        return Instance; 
    }
    private void OnTakeFromPool(Nail Instance){
        Instance.gameObject.SetActive(true);
        SpawnBullet(Instance);
    }
    private void OnReturnToPool(Nail Instance){
        Instance.gameObject.SetActive(false);
    }
    private void OnDestroyObject(Nail Instance){
        Destroy(Instance);
    }
    public void SpawnBullet(Nail Instance){
        Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 1f));

        Instance.transform.position = rayOrigin;
        Instance.transform.rotation = cam.transform.rotation;
    }
    public override void Shoot()
    {
        if(canShoot){
            
            canShoot=false;

            NailPool.Get();
           
            Invoke(nameof(ResetShot),0.1f);
        }
    }

    public void ResetShot(){
        canShoot=true;
    }
}
