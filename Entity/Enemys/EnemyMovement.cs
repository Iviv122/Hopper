using System.Collections;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] public Transform Target;
    public NavMeshAgent Agent;
    protected Coroutine FollowCoroutine;
    protected WaitForSeconds Wait;
    public void Awake() {
       Agent = GetComponent<NavMeshAgent>();
       Wait = GetComponent<Enemy>().UpdateSpeed; 
    }
    public void OnDisable(){
        Stop();
    }
    public void OnDestroy() {
        Stop();
    }
    public void Stop(){
        if(FollowCoroutine != null){
            StopCoroutine(FollowCoroutine);
            FollowCoroutine = null;
        }
        Agent.SetDestination(transform.position);
    }
    virtual public void Move()
    {
        if(FollowCoroutine == null){
            FollowCoroutine = StartCoroutine(FollowTarget()); 
        }
    }
    
    private IEnumerator FollowTarget(){

        while(enabled){
            Agent.SetDestination(Target.transform.position);
            yield return Wait;
        }
    }
}
