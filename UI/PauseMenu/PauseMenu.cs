using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] public InputManager input;
    [SerializeField] GameObject pausemenu;
    private bool isOn =false;
    private void Awake() {

        input.Pause += PauseSwitch;
        pausemenu.SetActive(isOn);
    }
    void PauseSwitch(){
        isOn = !isOn;
        pausemenu.SetActive(isOn);

        if(isOn){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        }else{
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        }
    }
    void Continue(){
        isOn = true;
        pausemenu.SetActive(isOn);
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }
    void Restart(){
        SceneManager.LoadScene(nameof(SceneManager.GetActiveScene));
    }
}
