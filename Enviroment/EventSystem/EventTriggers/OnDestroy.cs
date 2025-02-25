using UnityEngine;

public class OnDestroyTrigger : IEvent 
{
    [SerializeField] IAction[] actions; 
    private void OnDestroy() {
        CallEvent();
    } 
    public override void CallEvent(){
        foreach(IAction i in actions){
            i.Action();
        }
    }
}
