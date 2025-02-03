using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class InputManager : MonoBehaviour
{
    [SerializeField] KeyCode jump = KeyCode.Space;
    [SerializeField] KeyCode crouch = KeyCode.LeftControl;
    [SerializeField] KeyCode restart = KeyCode.R;
    public float x;
    public float y;

    public event Action Shoot;
    public event Action<int> InputNumber;
    public event Action InputRegistred;
    public event Action InputRestart;
    public void ControllWASD()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
    }
    public bool isJumping()
    {
        return Input.GetKey(jump);
    }
    public bool isCrouchingDown()
    {
        return Input.GetKeyDown(crouch);
    }
    public bool isCrouching()
    {
        return Input.GetKey(crouch);
    }
    public bool isCrouchingUp()
    {
        return Input.GetKeyUp(crouch);
    }
    private void LeftMouseButon()
    {
        if (Input.GetMouseButton(0))
        {
            Shoot?.Invoke();
        }
    }
    private void Restart(){
        if(Input.GetKeyDown(restart)){
            //InputRestart?.Invoke();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    private void NumberInput()
    {
        for (KeyCode key = KeyCode.Alpha0; key <= KeyCode.Alpha9; key++)
        {
            if (Input.GetKeyDown(key))
            {
                int numberPressed = key - KeyCode.Alpha0;
                InputNumber?.Invoke(numberPressed);
            }
        }
    }
    private void AnyInput(){
        if(Input.anyKeyDown || Input.anyKey){
            InputRegistred?.Invoke();
        }
    }
    public void Update()
    {
        AnyInput();

        NumberInput();
        LeftMouseButon();
        ControllWASD();
        Restart();
    }
}
