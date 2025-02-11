using UnityEngine;

public class RangeHomingTurrent : Entity 
{
    [SerializeField] GameObject Ammo;
    [SerializeField] float AttackInSecond;
    [SerializeField] float AttackRadius;
    [SerializeField] float BulletRadius;
    [SerializeField] LayerMask obstacles;
    [SerializeField] Transform player;
    Vector3 BulletOffset;
    RaycastHit Hit;
    void Awake()
    {
        BulletOffset = transform.forward*2;
    }
    void Update()
    {
        if(HasLineOfSight(player)){
            Shoot(player);
        }    
    }
    bool HasLineOfSight(Transform target){
        if(Physics.SphereCast(transform.position,BulletRadius,(target.position-transform.position).normalized,out Hit, AttackRadius,obstacles)){
            Entity prey;
            if (prey = Hit.collider.GetComponentInParent<PlayerInfo>())
            {
                return prey.gameObject.transform == target;
            }
        }
        return false;
    }
    public void Shoot(Transform target){
        Vector3 dir = (target.position-transform.position).normalized;

        Instantiate(Ammo, transform.position + dir*2f,Quaternion.LookRotation(dir,target.forward));
    }
    public override void Die(){
        // Drop Ammo
        //Explosion

        GameObject.Find("Player").GetComponent<PlayerInfo>().Health+=100;
        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position,AttackRadius);
    }
}
