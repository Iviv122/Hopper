using System;
using Unity.VisualScripting;
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
    [SerializeField] Transform target;
    [Header("Object Pool")]
    [SerializeField] int maxAmountOfObjects = 1000;
    [SerializeField] int startPool = 100;

    private ObjectPool<Projectile> ProjectilePool;
    private Quaternion rot;
    private void ReturnObjectToPool(Projectile Instance){
        ProjectilePool.Release(Instance);
    }
    private Projectile CreatePooledObject(){
        Projectile Instance = Instantiate(projectile, transform.position+offset, rot).GetComponent<Projectile>();
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
        Instance.transform.rotation = rot;
    }
    void Awake()
    {

        rot = Quaternion.LookRotation(transform.position+ShootDirection,transform.up);
        //rot = Quaternion.FromToRotation(transform.position+offset,ShootDirection+transform.position);
        ProjectilePool = new ObjectPool<Projectile>(CreatePooledObject,OnTakeFromPool,OnReturnToPool, OnDestroyObject,false,startPool,maxAmountOfObjects);
        InvokeRepeating(nameof(Shoot),delayBeforeStart,delayBetweenAttacks);
    }
    void Shoot(){
        ProjectilePool.Get();
    }
    private void OnDrawGizmosSelected() {
        rot = Quaternion.LookRotation(transform.position+ShootDirection,transform.up);
        Vector3 direction = rot * Vector3.forward;
        Gizmos.DrawLine(transform.position+offset,transform.position+offset+direction);
    }
}
