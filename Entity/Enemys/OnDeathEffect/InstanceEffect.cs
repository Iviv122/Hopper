using UnityEngine;

public class InstanceEffect : MonoBehaviour 
{
    [SerializeField] GameObject effect;

    private void OnDisable() {
        GameObject VFX = Instantiate(effect,transform.position,Quaternion.identity); 
        VFX.transform.parent = null;
        VFX.GetComponent<ParticleSystem>().Play();
    }        
}
