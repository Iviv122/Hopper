using Unity.VisualScripting;
using UnityEngine;

public class DamageFloor : MonoBehaviour
{
    [SerializeField] int damagePerInstance;
    private Entity victim;
    private void OnCollisionStay(Collision other) {
        if((victim = other.gameObject.GetComponent<Entity>()) != null){
            victim.GetDamage(damagePerInstance);
        }
        if((victim = other.gameObject.GetComponentInParent<Entity>()) != null){
            victim.GetDamage(damagePerInstance);
        }
    }
}
