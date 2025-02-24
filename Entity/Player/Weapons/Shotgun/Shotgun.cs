using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class Shotgun : Weapon
{

    private bool canShoot = true;
    private ObjectPool<Pellet> PelletPool;
    void Awake()
    {
        canShoot = true;

        Bullet = Resources.Load("Projectiles/PlayerPellet") as GameObject;
        ammoType = AmmoType.BuckShot;
        ammoConsumption = 1;

        PelletPool = new ObjectPool<Pellet>(CreatePooledObject,OnTakeFromPool,OnReturnToPool, OnDestroyObject,false,200,2000);
    }
    private void ReturnObjectToPool(Pellet Instance){
        PelletPool.Release(Instance);
    }
    private Pellet CreatePooledObject(){
        Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 1f));

        float AngleX = Random.Range(-5f,5.1f);
        float AngleY = Random.Range(-5f,5.1f);

        Pellet Instance = Instantiate(
            Bullet,
            rayOrigin,
            Quaternion.Euler(
                cam.transform.rotation.eulerAngles.x+AngleX,
                cam.transform.rotation.eulerAngles.y+AngleY,
                cam.transform.rotation.eulerAngles.z)
            ).GetComponent<Pellet>();
       
       
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
        float AngleX = Random.Range(-5f,5f);
        float AngleY = Random.Range(-5f,5f);
        Instance.transform.rotation = Quaternion.Euler(cam.transform.rotation.eulerAngles.x+AngleX,cam.transform.rotation.eulerAngles.y+AngleY,cam.transform.rotation.eulerAngles.z); 
        // Quaternion.Euler(cam.transform.rotation.x+AngleX,cam.transform.rotation.y+AngleY,cam.transform.rotation.z);
    }
    public override void Shoot(AmmoManager ammo)
    {
        if (canShoot){
            Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 1f));

            rb.linearVelocity += -cam.transform.forward*7.5f;
            //Instnce
            for(int i=0;i<20;i++){
                PelletPool.Get();
            }
            
            ammo.RemoveAmmo(ammoType,ammoConsumption);

            canShoot = false;
            Invoke(nameof(ResetShot),0.4f);
        }
    }
    public void ResetShot(){
        canShoot=true;
    }
}
