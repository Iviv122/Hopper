using UnityEngine;

public class ArmourMeter : MonoBehaviour
{
    [SerializeField] PlayerInfo player;
    private Quaternion target;
    private void Start() {
        player.ArmourChanged += UpdateArmour;        
    }
    void UpdateArmour(){
        target= Quaternion.Euler(-player.Armour*1.8f,0,0); 
    }
    void Update(){
    transform.localRotation = Quaternion.Lerp(transform.localRotation,target,Time.deltaTime*2f); 
    }
}
