using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;
using UnityEngine.UIElements;
/// <summary>
/// As well as turret, only one active target so sad :p 
/// </summary>
public class BehaviorSimpleRange : MonoBehaviour
{
    [Header("Base")]
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] public Transform CurrentTarget;
    [SerializeField] public float timeBetweenAttacks;
    [SerializeField] float AttackRadius;
    [SerializeField] float BulletRadius;
    [SerializeField] LayerMask obstacles;
    [SerializeField] LayerMask whatIsGround, whatIsPlayer;
    [Header("Pool")]
    public GameObject Bullet;
    public int startPool;
    public int maxProjectiles;
    [Header("Radiuces")]
    public bool playerInSightRange;
    public bool playerInAttackRange; 
    public Vector3 walkPoint;
    public float walkPointRange;

    public float sightRange;
    
    ObjectPool<Projectile> ProjectilePool;
    RaycastHit Hit;
    bool walkPointSet;
    bool alreadyAttacked;
    
    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position,AttackRadius);
        Gizmos.DrawWireSphere(transform.position,sightRange);
    }
    void Awake(){
        CurrentTarget = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        ProjectilePool = new ObjectPool<Projectile>(CreatePooledObject,OnTakeFromPool,OnReturnToPool, OnDestroyObject,false,startPool,maxProjectiles);
    }
    private void ReturnObjectToPool(Projectile Instance){
        ProjectilePool.Release(Instance);
    }
    private Projectile CreatePooledObject(){

        Projectile Instance = Instantiate(Bullet,transform.position+transform.forward*3,transform.rotation).GetComponent<Projectile>();
        
        Instance.Disable += (Projectile p) => ReturnObjectToPool(p as Projectile);
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
        Instance.Spawn(CurrentTarget);

        Instance.transform.position = transform.position+transform.forward*3;
        Instance.transform.rotation = transform.rotation;
    }
    void SearchWalkPoint(){
        float randomZ = Random.Range(-walkPointRange,walkPointRange);
        float randomX = Random.Range(-walkPointRange,walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX,transform.position.y,transform.position.z+randomZ);
    
        if(Physics.Raycast(walkPoint,-transform.up,2f,whatIsGround)){
            walkPointSet=true;
        }
    }
    void Patrolling(){
        if(!walkPointSet) SearchWalkPoint();
        if(walkPointSet) agent.SetDestination(walkPoint);

       Vector3 distanceToWalkPoint = transform.position - walkPoint; 
        if(distanceToWalkPoint.magnitude < 1f){
            walkPointSet = false;
        }
    }
    void ChasePlayer(){
        agent.SetDestination(CurrentTarget.position);
    }
    void ResetAttack(){
        alreadyAttacked = false;
    }
    void AttackPlayer(){
        agent.SetDestination(transform.position);

        transform.LookAt(CurrentTarget);

        if(!alreadyAttacked){
            /// Attack Code go here

            //Debug.Log("I shot you, you dead"); 
            //GameObject Instance = Instantiate(Bullet,transform.position+transform.forward*3,transform.rotation);
            //Instance.GetComponent<Projectile>().Spawn(CurrentTarget);
            ProjectilePool.Get();
            ///
         
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack),timeBetweenAttacks);
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
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position,sightRange,whatIsPlayer);
        playerInAttackRange = HasLineOfSight(CurrentTarget);

        if (!playerInSightRange && !playerInAttackRange){Patrolling();}
        if (playerInSightRange && !playerInAttackRange){ChasePlayer();}
        if (playerInSightRange && playerInAttackRange){AttackPlayer();}
    }
}
