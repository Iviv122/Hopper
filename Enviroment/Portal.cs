using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{

    [Scene]
    [SerializeField] string goTo;

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.GetComponentInParent<PlayerInfo>()){
            SceneManager.LoadScene(goTo);
        }
    }

}
