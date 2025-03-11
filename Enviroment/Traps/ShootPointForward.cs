using UnityEngine;
using UnityEngine.Pool;

public class ShootPointForward : MonoBehaviour
{
    [Header("Base")]
    [SerializeField] GameObject projectile;
    [SerializeField] float AttackDelay;
    [SerializeField] float AttackCooldown;
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;
    [Header("Pool")]
    [SerializeField] int maxAmountOfObjects = 1000;
    [SerializeField] int startPool = 100;
    private ObjectPool<Projectile> ProjectilePool;

    private void ReturnObjectToPool(Projectile Instance){
        ProjectilePool.Release(Instance);
    }
    private Projectile CreatePooledObject(){
        Projectile Instance = Instantiate(projectile, transform.position+offset, transform.rotation).GetComponent<Projectile>();
        Instance.Disable += (Projectile p) => ReturnObjectToPool(p as Projectile);
        Instance.Spawn(target); 
        Instance.gameObject.SetActive(true);

        return Instance; 
    }
    private void OnTakeFromPool(Projectile Instance){
        Instance.gameObject.SetActive(true);
        SpawnBullet(Instance);
    }
    private void OnReturnToPool(Projectile Instance){
        Instance.gameObject.SetActive(false);
    }
    private void OnDestroyObject(Projectile Instance){
        Destroy(Instance);
    }
    public void SpawnBullet(Projectile Instance){
        Instance.Spawn(target); 

        Instance.transform.position = transform.position+offset;
        Instance.transform.rotation = transform.rotation;
    }
    void Awake(){
        ProjectilePool = new ObjectPool<Projectile>(CreatePooledObject,OnTakeFromPool,OnReturnToPool, OnDestroyObject,false,startPool,maxAmountOfObjects);
        InvokeRepeating(nameof(Shoot),AttackDelay,AttackCooldown);    
    }
    void Shoot(){
        ProjectilePool.Get();
    }
    private void OnDrawGizmosSelected() {
        Gizmos.DrawLine(transform.position+offset,transform.position+transform.forward*100);     
    }
}
