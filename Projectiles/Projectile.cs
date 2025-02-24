using UnityEngine;

abstract public class Projectile : MonoBehaviour
{
    [SerializeField] public ParticleSystem emit;
    [SerializeField] public Vector3 localScaleEmit; 
    public delegate void OnDisable(Projectile Instance);
    public OnDisable Disable;
    abstract public void Spawn(Transform target);
    virtual public void DestroyBreakable(Transform pos,float radius, int damage){
        Collider[] cols = Physics.OverlapSphere(pos.position,radius);

        foreach (Collider item in cols)
        {
            if(item.gameObject.TryGetComponent<BlowAble>(out BlowAble prey)){
                prey.Health-=damage;
            }
        }
    }
    public void DetachParticles() 
    {
        if (emit == null)
        {
            return;
        }
   
        emit.transform.parent = null;
        emit.transform.localScale = localScaleEmit;
        emit.Stop();
    }

}
