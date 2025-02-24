using TMPro;
using UnityEngine;
using UnityEngine.Pool;
/// <summary>
/// Shoots when TARGET within radius and Can have only setted one target!!! 
/// </summary>
public class RangeHomingTurrent : Entity 
{
    [Header("Base")]
    [SerializeField] GameObject Ammo;
    [SerializeField] float AttackInSecond;
    [SerializeField] float AttackRadius;
    [SerializeField] float BulletRadius;
    [SerializeField] LayerMask obstacles;
    [SerializeField] public Transform CurrentTarget;
    
    [Header("ObjectPool")]
    [SerializeField] int startPool = 100;
    [SerializeField] int maxProjectiles = 200;
    ObjectPool<Projectile> ProjectilePool;
    bool readyToShoot;
    RaycastHit Hit;
    void Awake()
    {
        readyToShoot = true;

        ProjectilePool = new ObjectPool<Projectile>(CreatePooledObject,OnTakeFromPool,OnReturnToPool, OnDestroyObject,false,startPool,maxProjectiles);    
    }
    private void ReturnObjectToPool(Projectile Instance){
        ProjectilePool.Release(Instance);
    }
    private Projectile CreatePooledObject(){

        Vector3 dir = (CurrentTarget.position-transform.position).normalized;

        Projectile Instance = Instantiate(Ammo, transform.position + dir*4f,Quaternion.LookRotation(dir,CurrentTarget.forward)).GetComponent<Projectile>();
        
        Instance.Disable += (Projectile p) => ReturnObjectToPool(p as Projectile);
        Instance.Spawn(transform);
        Instance.gameObject.SetActive(true);

        return Instance; 
    }
    private void OnTakeFromPool(Projectile Instance){
        Instance.gameObject.SetActive(true);
        Instance.Spawn(CurrentTarget);
        SpawnBullet(Instance);
    }
    private void OnReturnToPool(Projectile Instance){
        Instance.gameObject.SetActive(false);
    }
    private void OnDestroyObject(Projectile Instance){
        Destroy(Instance);
    }
    public void SpawnBullet(Projectile Instance){
        Vector3 dir = (CurrentTarget.position-transform.position).normalized;
        Instance.Spawn(CurrentTarget);

        Instance.transform.position = transform.position + dir*4f;
        Instance.transform.rotation = Quaternion.LookRotation(dir,CurrentTarget.forward);
    }
    void Update()
    {
        if(HasLineOfSight(CurrentTarget) && readyToShoot){
            Shoot();
        }    
    }
    bool HasLineOfSight(Transform target){
        if(Physics.SphereCast(transform.position,BulletRadius,(target.position-transform.position).normalized,out Hit, AttackRadius,obstacles)){
            Entity prey;
            if (prey = Hit.collider.GetComponentInParent<PlayerInfo>())
            {
                return prey.gameObject.transform == target;
            }
        }
        return false;
    }
    public void Shoot(){
        //Vector3 dir = (CurrentTarget.position-transform.position).normalized;
        //Instantiate(Ammo, transform.position + dir*2f,Quaternion.LookRotation(dir,CurrentTarget.forward));
        ProjectilePool.Get(); 
        readyToShoot = false;
        Invoke(nameof(ReadyToShoot),1/AttackInSecond);
    }
    void ReadyToShoot(){
        readyToShoot = true;
    }
    public override void Die(){
        // Drop Ammo
        //Explosion

        GameObject.Find("Player").GetComponent<PlayerInfo>().Health+=100;
        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position,AttackRadius);
    }
}
