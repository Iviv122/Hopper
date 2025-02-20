using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject Enemy;
    [SerializeField] NavMeshSurface ground;  
    [SerializeField] Vector3 pos;
    [SerializeField] float spawnDelay; 
    private void Awake() {
     if(spawnDelay != 0){
        InvokeRepeating(nameof(Spawn),0,spawnDelay);
     }else{
        Invoke(nameof(Spawn),0);
     } 
    }
    public void Spawn(){
        Instantiate(Enemy,transform.position+pos,Quaternion.identity);
    } 
    private void OnDrawGizmosSelected() {
        Gizmos.DrawCube(transform.position+pos,new Vector3(0.2f,0.2f,0.2f));     
    }
}
