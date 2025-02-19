using UnityEngine;

public class OnUse : IEvent 
{
    [SerializeField] IAction[] actions; 
    public override void CallEvent(){
        foreach(IAction i in actions){
            i.Action();
        }
    }
}
