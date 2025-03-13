using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnemyMovement))]
public class BasicRangeEnemy : Enemy 
{
    public float AttackRadius = 2f;
    public float AttackDelay = 1.5f;
    public float ProjectileRadius = 0.3f;
    public LayerMask masksToCheck;

    private WaitForSeconds Wait;
    private Coroutine attackCoroutine;

    private EnemyStates state;

    public override void OnEnable()
    {
        base.OnEnable();

        Wait = new WaitForSeconds(AttackDelay);

        
    }
    public void Start(){
        StartCoroutine(Logic());
        attackCoroutine = null;
        state = EnemyStates.Idle;
    }
    [SerializeField]private bool inAttackRadius = false;
    [SerializeField]private bool inSight = false;
    public IEnumerator Logic(){
        while(true){

            inAttackRadius = InRadius(Target,AttackRadius);
            inSight = inAttackRadius ? HasLineOfSightTo(Target,AttackRadius,ProjectileRadius,masksToCheck) : false;
            
            HandleState();
            
            HandleAction();

            yield return UpdateSpeed;
        }
    }
    public void HandleState(){
        if(inSight){
            state = EnemyStates.Attack; 
        }else if(attackCoroutine != null){
            state = EnemyStates.Attack;
        }else if(!inSight && attackCoroutine == null){
            state = EnemyStates.Chase;
        } if(Target == null){
            state = EnemyStates.Idle;
        }
    }
    public void HandleAction(){
        switch (state)
        {
            case EnemyStates.Attack:
                movement.Stop();
                attackCoroutine ??= StartCoroutine(AttackRoutine());
            break;
            case EnemyStates.Chase:
                movement.Move();
            break;
            case EnemyStates.Idle:
                movement.Stop();
            break;
            default:
                movement.Stop();
            break;
        }
    }
    public IEnumerator AttackRoutine(){
        Debug.Log("Delay before attack"); 
        movement.Stop();
        yield return Wait;
        
        movement.Stop();
        Attack();
        Debug.Log("attack"); 
       
        yield return Wait;
        Debug.Log("Delay after attack");
        attackCoroutine = null;
    }
    public void StopAttack(){
        StopCoroutine(attackCoroutine);
        attackCoroutine = null;
    }
    public void Attack(){
        if(HasLineOfSightTo(Target,99999,ProjectileRadius,masksToCheck) ){
            attackManager.AttackRange(Target);
        }
    }
    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(gameObject.transform.position,AttackRadius);
        Gizmos.DrawWireSphere(gameObject.transform.position,ProjectileRadius);
    } 
}
