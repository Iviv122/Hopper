using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnemyMovement))]
public class BasicMeleeEnemy : Enemy 
{
    public float AttackRadius = 2f;
    public float PreAttackDelay = 0.6f;
    public float PostAttackDelay = 0.2f;
    public Animator animator;

    private WaitForSeconds PreWait;
    private WaitForSeconds PostWait;
    private Coroutine attackCoroutine;
    public override void OnEnable()
    {
        base.OnEnable();

        
    }
    public void Start(){
        PreWait = new WaitForSeconds(PreAttackDelay);
        PostWait = new WaitForSeconds(PostAttackDelay);

        StartCoroutine(Logic());
        attackCoroutine = null;
    }
    public IEnumerator Logic(){
        while(true){

            // In Range, stop attack
            // else go to target
            if(InRadius(Target,AttackRadius) && attackCoroutine == null){
                movement.Stop();
                attackCoroutine = StartCoroutine(AttackRoutine());
            }else if(attackCoroutine != null){
                movement.Stop();
            }else{
                movement.Move();
            } 

            yield return UpdateSpeed;
        }
    }
    public IEnumerator AttackRoutine(){
        animator.SetTrigger("Attack");
        Debug.Log("Delay before attack"); 
        yield return PreWait;
        Attack();
        Debug.Log("attack"); 
       
        yield return PostWait;
        Debug.Log("Delay after attack");
        attackCoroutine = null;
    }
    public void Attack(){
        if(InRadius(Target,AttackRadius)){
            attackManager.AttackMelee(Target);
        }
    }
    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(gameObject.transform.position,AttackRadius);
    } 
}
