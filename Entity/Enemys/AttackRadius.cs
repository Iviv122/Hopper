using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SphereCollider))]
public class AttackRadius : MonoBehaviour
{
    public SphereCollider Collider;
    protected List<Entity> targets = new();
    public int Damage = 10;
    public float AttackDelay = 0.5f;
    public delegate void AttackEvent(Entity target);
    public AttackEvent OnAttack;
    protected Coroutine AttackCoroutine;

    protected virtual void Awake(){
        Collider = GetComponent<SphereCollider>();
    }
    protected virtual void OnTriggerEnter(Collider other) {
        PlayerInfo player = other.GetComponentInParent<PlayerInfo>(); 
        if(player != null){
            targets.Add(player);
            if(AttackCoroutine == null){
                AttackCoroutine = StartCoroutine(Attack());
            }
        } 
    }
    protected virtual void OnTriggerExit(Collider other) {
        PlayerInfo player = other.GetComponentInParent<PlayerInfo>(); 
        if(player != null){
            targets.Remove(player);
            if(targets.Count == 0){
                StopCoroutine(AttackCoroutine);
                AttackCoroutine = null;
            }
        }
    }
    protected virtual IEnumerator Attack(){
        WaitForSeconds Wait = new WaitForSeconds(AttackDelay);

        yield return Wait;

        Entity closestDamagable = null;
        float closestDistance = float.MaxValue;

        while (targets.Count > 0)
        {
            for(int i=0;i<targets.Count;i++){
                Transform targetTransform = targets[i].transform;

                float distance = Vector3.Distance(transform.position,targetTransform.position);

                if(distance < closestDistance){
                    closestDistance = distance;
                    closestDamagable = targets[i]; 
                }
            }
            if(closestDamagable != null){
                OnAttack?.Invoke(closestDamagable);
                closestDamagable.GetDamage(Damage);
            }
            
            closestDamagable = null;
            closestDistance = float.MaxValue;
        
            yield return Wait;

            //targets.RemoveAll(DisabledTargets);
        }
        AttackCoroutine = null;
    } 

    protected bool DisabledTargets(Entity target){
        return target != null && !target.transform.gameObject.activeSelf; 
    }
}
