using UnityEngine;

public class PickUpSpawn : MonoBehaviour
{
    [Header("Base")]
    [SerializeField] GameObject prefab;
    [SerializeField] Vector3 pos;
    [SerializeField] float cooldown;
    [SerializeField] bool spawnOnce;

    [Header("Renderer to Turn off")]
    [SerializeField] Renderer mesh;

    // Private stuff
    private GameObject cur; 
    private IDeleteEvent eventListener;
    void Awake()
    {
        if(mesh != null){
            mesh.enabled = false;
        }
        if(!spawnOnce){
        Spawn();
        }else{
        SingleSpawn();
        Destroy(gameObject);
        }
    }
    void StartSpawning(){
        eventListener.Destroyed -= StartSpawning;
        Invoke(nameof(Spawn),cooldown);
    }
    void Spawn(){
        cur = Instantiate(prefab,transform.position+pos,Quaternion.identity);
        eventListener = cur.AddComponent<IDeleteEvent>();
        eventListener.Destroyed += StartSpawning;
    }
    void SingleSpawn(){
        cur = Instantiate(prefab,transform.position+pos,Quaternion.identity);
    }
    private void OnDrawGizmosSelected() {
       Gizmos.DrawCube(transform.position+pos,new Vector3(0.1f,0.1f,0.1f)); 
    }
}
