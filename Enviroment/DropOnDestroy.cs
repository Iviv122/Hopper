using UnityEngine;

public class DropOnDestroy : MonoBehaviour
{
    [SerializeField] GameObject PickUp;
    [SerializeField] Vector3 pos;
    private void OnDestroy() {
        if(!this.gameObject.scene.isLoaded) return;
        Instantiate(PickUp, transform.position+pos, Quaternion.identity);
    }
    private void OnDrawGizmosSelected() {
        Gizmos.DrawCube(transform.position+pos,new Vector3(0.1f,0.1f,0.1f));
    }
}
