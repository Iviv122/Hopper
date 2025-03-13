using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [Header("Base")]
    public Transform Player;
    public Vector3 SpawnPos;
    public int NumberOfEnemies = 5;
    public float SpawnDelay = 1f;
    public GameObject EnemyPrefab;

    [Header("Additional Settings")]
    public bool isContiniusSpawning;

    private NavMeshTriangulation Triangulation;
    private ObjectPool<Enemy> EnemyPool;
    [SerializeField]private int SpawnedEnemies;
    private void Awake()
    {
        SpawnPos += transform.position;
        EnemyPool = new ObjectPool<Enemy>(CreatePooledObject, OnTakeFromPool, OnReturnToPool, OnDestroyObject, false, NumberOfEnemies, NumberOfEnemies);
    
        Triangulation = NavMesh.CalculateTriangulation();
    }
    private void Start(){
    
        StartCoroutine(SpawnEnemies());
    }
    private IEnumerator SpawnEnemies(){
        WaitForSeconds Wait = new WaitForSeconds(SpawnDelay);

        SpawnedEnemies = 0;
        if(isContiniusSpawning){
            while(true){
                if(SpawnedEnemies < NumberOfEnemies){
                    Spawn();
                    SpawnedEnemies++;
                }
            yield return Wait;
            }
        }else{
            while(SpawnedEnemies < NumberOfEnemies){
                    Spawn();
                    SpawnedEnemies++;
                
            yield return Wait;
            }
        }                
    }
    public void Spawn(){
        Enemy enemy = EnemyPool.Get();
        if(enemy != null){

            NavMeshHit Hit;
            if(NavMesh.SamplePosition(SpawnPos,out Hit,200f,-1)){
                //literally teleport
                enemy.movement.Agent.Warp(Hit.position);
                //turn on 
                enemy.Setup(Player);
            }else{
                Debug.Log($"Warping enemy to {Hit.position}");
                Debug.LogError($"Can`t place NavMeshAgent on NavMesh");
            } 
        }else{
            Debug.LogError($"Unable to fetch enemy");
        }
    }
    private void ReturnObjectToPool(Enemy enemy)
    {
        EnemyPool.Release(enemy);
    }
    private void OnReturnToPool(Enemy enemy){
        enemy.gameObject.SetActive(false);
        if(isContiniusSpawning){
            SpawnedEnemies--;
        }
    }
    private void OnDestroyObject(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }
    private Enemy CreatePooledObject()
    {
        Enemy enemy = Instantiate(EnemyPrefab,SpawnPos,Quaternion.identity).GetComponent<Enemy>(); 
        enemy.Health = enemy.MaxHealth;


        enemy.Disable += (Enemy e) => ReturnObjectToPool(e);

        enemy.gameObject.SetActive(true);

        return enemy; 
    }

    private void OnTakeFromPool(Enemy enemy)
    {
       enemy.gameObject.SetActive(true); 

    }
    private void OnDrawGizmosSelected() {
        Gizmos.DrawCube(SpawnPos+transform.position,new Vector3(0.1f,.1f,.1f));
    }
}
