using System;
using UnityEngine;
using UnityEngine.Rendering;

public class Interact : MonoBehaviour
{
    [SerializeField] LayerMask activeLayers;
    [SerializeField] InputManager input;
    [SerializeField] GameObject coursor;
    private RaycastHit hit;
    IEvent act;
    private void Awake() {
        input.Act += OnAct;
        coursor.SetActive(false);
    }
    void OnAct(){
        if(act != null){
            act.CallEvent();
        }
    }
    void Update()
    {
        
        coursor.SetActive(false);
        if(Physics.Raycast(transform.position,transform.forward,out hit, 3.5f,activeLayers)){
            if((act = hit.collider.GetComponent<IEvent>()) != null){
                //Debug.Log("You can interact now");
                coursor.SetActive(true);
            }
        }
    }
    private void OnDrawGizmosSelected() {
        Gizmos.DrawLine(transform.position,transform.position+transform.forward*3.5f);
    }
}
