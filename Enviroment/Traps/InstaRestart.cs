using UnityEngine;
using UnityEngine.SceneManagement;

public class InstaRestart : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {

        if(other.gameObject.GetComponentInParent<PlayerInfo>()){
          SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        }
    }
    private void OnTriggerEnter(Collider other) {

        if(other.gameObject.GetComponentInParent<PlayerInfo>()){
           SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }        
}