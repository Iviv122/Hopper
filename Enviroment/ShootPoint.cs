using System;
using UnityEngine;
using UnityEngine.Pool;

public class ShootPoint : MonoBehaviour
{
    [Header("Basic")]
    [SerializeField] GameObject projectile;
    [SerializeField] float delayBeforeStart;
    [SerializeField] float delayBetweenAttacks;
    [SerializeField] Vector3 ShootDirection;
    [SerializeField] Vector3 offset;
    [Header("Object Pool")]
    [SerializeField] float maxAmountOfObjects;
    private ObjectPool<Projectile> ProjectilePool;
    private void ReturnObjectToPool(Projectile Instance){
        ProjectilePool.Release(Instance);
    }
    private Projectile CreatePooledObject(){
        Projectile Instance = Instantiate(projectile, transform.position+offset, Quaternion.FromToRotation(transform.position,ShootDirection)).GetComponent<Projectile>();
        Instance.Disable += (Projectile p) => ReturnObjectToPool(p as Projectile);
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
        throw new NotImplementedException();
    }
    void Awake()
    {
        ProjectilePool = new ObjectPool<Projectile>(CreatePooledObject,OnTakeFromPool,OnReturnToPool, OnDestroyObject,false,200,100_000);
        InvokeRepeating(nameof(Shoot),delayBeforeStart,delayBetweenAttacks);
    }
    void Shoot(){

    }
    private void OnDrawGizmosSelected() {
        Gizmos.DrawLine(ShootDirection.normalized*100,transform.position);
    }
}
