using UnityEngine;

public class AttackTransform : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        other.transform.SetParent(transform);
    }

    private void OnCollisionExit(Collision other) {
        other.transform.SetParent(null);
    }   
}
