using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class BehaviorSimpleRange : MonoBehaviour
{
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] public Transform player;
    [SerializeField] public float timeBetweenAttacks;
    [SerializeField] float AttackRadius;
    [SerializeField] float BulletRadius;
    [SerializeField] LayerMask obstacles;
    [SerializeField] LayerMask whatIsGround, whatIsPlayer;
   
    RaycastHit Hit;
    public GameObject Bullet;
    
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    bool alreadyAttacked;

    public float sightRange;
    public bool playerInSightRange, playerInAttackRange; 

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position,AttackRadius);
        Gizmos.DrawWireSphere(transform.position,sightRange);
    }
    void Awake(){
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
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
        agent.SetDestination(player.position);
    }
    void ResetAttack(){
        alreadyAttacked = false;
    }
    void AttackPlayer(){
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if(!alreadyAttacked){
            /// Attack Code go here

            Debug.Log("I shot you, you dead"); 
            GameObject Instance = Instantiate(Bullet,transform.position+transform.forward*3,transform.rotation);
            Instance.GetComponent<Projectile>().Spawn(player);
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
        playerInAttackRange = HasLineOfSight(player);

        if (!playerInSightRange && !playerInAttackRange){Patrolling();}
        if (playerInSightRange && !playerInAttackRange){ChasePlayer();}
        if (playerInSightRange && playerInAttackRange){AttackPlayer();}
    }
}
