using System.Collections;
using System.Linq;
using UnityEngine;

public class EmptyHands : Weapon
{
    [SerializeField] private GameObject AnimatedPunch;
    private bool canShoot = true;
    private Vector3 HalfSize = new Vector3(0.1f, 0.1f, 0.75f);

    [SerializeField] private float HitDelay = 0.1f;
    [SerializeField] private float AnimationLength = 0.3f;

    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 endPos;

    Coroutine coroutineAnim;
    float startTime;
    public void Awake()
    {
        ammoType = AmmoType.None;
    }
    private void ResetShot()
    {
        canShoot = true;
    }
    private IEnumerator StartAnimation()
    {
        float elapsedTime;  // Calculate elapsed time

        while(true){
            elapsedTime = Time.time - startTime;
            if (elapsedTime < HitDelay)
            {
                AnimatedPunch.transform.localPosition = Vector3.Lerp(startPos,endPos,elapsedTime/HitDelay);  // Apply size to the sphere
            }
            else{
                AnimatedPunch.transform.localPosition = Vector3.Lerp(endPos,startPos,elapsedTime/AnimationLength);  // Apply size to the sphere
            }
            yield return null;
        }
    }
    private void StopAnimation(){
        StopCoroutine(coroutineAnim);
        AnimatedPunch.transform.localPosition = startPos;
    }
    public void Punch()
    {
        Collider[] cols = Physics.OverlapBox(cam.transform.position + cam.transform.forward, HalfSize, cam.transform.rotation);
        foreach (Collider item in cols)
        {
            if (item.TryGetComponent(out Entity prey))
            {
                if (prey is PlayerInfo)
                {
                    continue;
                }
                else
                {
                    prey.GetDamage(3);
                }
            }
            if (item.TryGetComponent(out PunchAble breakObj))
            {
                breakObj.Health -= 3;
            }
        }
    }
    public override void Shoot(AmmoManager ammo)
    {
        if (canShoot == true)
        {
            startTime = Time.time;

            startPos = AnimatedPunch.transform.localPosition;
            endPos = AnimatedPunch.transform.localPosition + new Vector3(0,0,20);

            coroutineAnim = StartCoroutine(StartAnimation()); 
            Invoke(nameof(Punch), HitDelay);
            Invoke(nameof(StopAnimation), AnimationLength);

            canShoot = false;
            Invoke(nameof(ResetShot), 0.8f);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(cam.transform.position + cam.transform.forward, HalfSize * 2);
    }
}
