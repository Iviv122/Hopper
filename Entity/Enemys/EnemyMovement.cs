using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] public Transform Target;
    [SerializeField] public float UpdateSpeed = 0.1f; // how often to recalculate path
    private NavMeshAgent Agent;
    
    private Coroutine FollowCoroutine;
    private void Awake() {
       Agent = GetComponent<NavMeshAgent>(); 
    }
    public void OnDisable(){
        StopCoroutine(FollowCoroutine);
        FollowCoroutine = null;
    }
    private void OnDestroy() {
        StopCoroutine(FollowCoroutine);
        FollowCoroutine = null;
    }
    public void StartChasing()
    {
        if(FollowCoroutine == null){
            FollowCoroutine = StartCoroutine(FollowTarget()); 
        }else{
            Debug.LogError("StartChasing was called multiple times, something went wrong");
        }
    }
    private IEnumerator FollowTarget(){
        WaitForSeconds Wait = new WaitForSeconds(UpdateSpeed);

        while(enabled){
            Agent.SetDestination(Target.transform.position);
            yield return Wait;
        }
    }
}
