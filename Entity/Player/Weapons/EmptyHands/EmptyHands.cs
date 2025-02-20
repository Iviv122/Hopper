using UnityEngine;

public class EmptyHands : Weapon
{
    private bool canShoot = true;
    private Vector3 HalfSize = new Vector3(0.1f,0.1f,0.75f);
    public void Awake(){
        ammoType = AmmoType.None; 
    }
    private void ResetShot(){
        canShoot = true;
    }
    public void Punch(){
        Collider[] cols = Physics.OverlapBox(cam.transform.position + cam.transform.forward,HalfSize,cam.transform.rotation);
        foreach (Collider item in cols)
        {
           if(item.TryGetComponent(out Entity prey)){
                if(prey is PlayerInfo){
                    continue;
                }else{
                    prey.GetDamage(3);
                }
           }
            if(item.TryGetComponent(out PunchAble breakObj)){
               breakObj.Health-=3; 
           } 
        }
    }
    public override void Shoot(AmmoManager ammo)
    {
        if(canShoot == true){

            Punch();
            canShoot = false;
        Invoke(nameof(ResetShot),0.8f);
        }
    }
    private void OnDrawGizmosSelected() {
        Gizmos.DrawCube(cam.transform.position + cam.transform.forward,HalfSize*2);
    } 
}
