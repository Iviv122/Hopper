using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnemyMovement))]
public class BasicMeleeEnemy : Enemy 
{
    public float AttackRadius = 2f;
    public float AttackDelay = 0.5f;

    private WaitForSeconds Wait;
    private Coroutine attackCoroutine; 
    public override void OnEnable()
    {
        base.OnEnable();

        
    }
    public void Start(){
        Wait = new WaitForSeconds(AttackDelay);

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
        
        Debug.Log("Delay before attack"); 
        yield return Wait;

        Attack();
        Debug.Log("attack"); 
       
        yield return Wait;
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
