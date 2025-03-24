using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Scene]
    public string FirstLevel;

    public GameObject SettingsWindow; 
    public void Play(){
        SceneManager.LoadScene(FirstLevel);
    }
    public void Settings(){
        SettingsWindow.SetActive(!SettingsWindow.activeSelf);
    }
    public void Exit(){
        Application.Quit();
    } 
}
enum MenuState{
    MainMenu,
    Settings,
}