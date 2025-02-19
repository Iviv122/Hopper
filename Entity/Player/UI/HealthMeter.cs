using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class HealthMeter : MonoBehaviour
{
    [SerializeField] PlayerInfo player;
    private Quaternion target;
    private void Start() {
        player.HealthChanged += UpdateHealth;        
    }
    void UpdateHealth(){
        target= Quaternion.Euler(-player.Health*1.8f,0,0); 
    }
    void Update(){
    transform.localRotation = Quaternion.Lerp(transform.localRotation,target,Time.deltaTime*2f); 
    }
}
