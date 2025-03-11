using UnityEngine;

public class EnableObject : IAction 
{
    [SerializeField] GameObject[] targets;
    public override void Action()
    {
        foreach (GameObject item in targets)
        {
            item.SetActive(true);
        }   
    }
}
