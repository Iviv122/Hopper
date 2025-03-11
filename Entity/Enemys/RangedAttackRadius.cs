using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class RangedAttackRadius : AttackRadius 
{
    public NavMeshAgent Agent;
    public GameObject Bullet;   
    public Vector3 BulletSpawnPos = new Vector3(0,0,0.7f);
    public LayerMask Mask;

    [SerializeField] private float sphereCastRadius = 0.15f;
    private RaycastHit Hit;
    private Entity Target;

    protected override void Awake(){
        base.Awake();

    }
    protected override IEnumerator Attack()
    {
        WaitForSeconds Wait = new WaitForSeconds(AttackDelay);
        

        while(targets.Count>0){
            for(int i=0;i<targets.Count;i++){
                if(HasLineOfSightTo(targets[i].gameObject.transform)){
                    Target = targets[i];
                    OnAttack?.Invoke(targets[i]);
                    Agent.enabled = false;
                    break; 
                }
            }

            if(Target != null){
                Agent.enabled = false;
                yield return Wait; 

                Vector3 dir = (Target.transform.position-transform.position).normalized;
                
                GameObject bul = Instantiate(
                    Bullet,
                    transform.position + BulletSpawnPos + dir*2,
                    Quaternion.LookRotation(dir,Target.transform.forward)
                    );
                bul.GetComponent<Projectile>().Spawn(Target.transform);

            }else{
                Agent.enabled = true;
            }
        
            yield return Wait;
            if(Target == null || !HasLineOfSightTo(Target.transform)){
                Agent.enabled = true;
                AttackCoroutine = null;
            }
        }
        
    }
    private bool HasLineOfSightTo(Transform _Target){
        
        if(Physics.SphereCast(transform.position + BulletSpawnPos,sphereCastRadius,(_Target.position-transform.position + BulletSpawnPos).normalized,out Hit, Collider.radius,Mask)){
            Entity prey;
            if(prey = Hit.collider.GetComponentInParent<PlayerInfo>()){
                return prey.gameObject.transform == _Target;
            }
        }

        return false; 
    } 
}
