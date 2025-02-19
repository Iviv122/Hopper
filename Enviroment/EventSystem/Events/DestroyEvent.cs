using UnityEngine;

public class DestroyEvent : IAction 
{
    [SerializeField] GameObject target;
    public override void Action(){
        Destroy(target);
    }
}
