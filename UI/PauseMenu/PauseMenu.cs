using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] public InputManager input;
    [SerializeField] GameObject pausemenu;
    [SerializeField] GameObject settingsMenu;
    [Scene] public string MainMenu;
    private bool isOn =false;
    private void Awake() {

        input.Pause += PauseSwitch;
        input.Restart += Restart;
        pausemenu.SetActive(isOn);
    }
    void PauseSwitch(){
        isOn = !isOn;
        pausemenu.SetActive(isOn);
        settingsMenu.SetActive(false);

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
    public void Continue(){
        isOn = false;
        pausemenu.SetActive(false);
        settingsMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }
    public void OpenSettings(){
        settingsMenu.SetActive(true);
    }
    public void Restart(){
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GoToMainMenu(){
        Time.timeScale = 1;
        SceneManager.LoadScene(MainMenu);
    }
}
