using UnityEngine;

public class DebugEvent : IAction 
{
    [SerializeField] string message;
    public override void Action(){
        Debug.Log(message);
    }
}

