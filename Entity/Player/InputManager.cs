using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    [SerializeField] private KeyCode slide = KeyCode.LeftControl;
    [SerializeField] private KeyCode run = KeyCode.LeftShift;
    [SerializeField] public float x;
    [SerializeField] public float y; 
    [SerializeField] public bool isCrouching;
    [SerializeField] public bool isRunning;

    public event Action CrouchStart;
    public event Action CrouchEnd;
    public event Action Shoot;
    public event Action Act;
    public event Action Jump;
    public event Action Pause;
    public event Action<int> InputNumber;
    public event Action anyInput;
    void MoveInput(){
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical"); 
    }
    void StartCrouch(){
        if(Input.GetKeyDown(slide)){
            CrouchStart?.Invoke();
        }
    }
    void IsCrouching(){
        isCrouching = Input.GetKey(slide);
    }
    void OnJump(){
        Jump?.Invoke();
    }
    void IsShoot(){
        if(Input.GetMouseButton(0)){
            Shoot?.Invoke();
        }
    }
    void OnRestart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void OnPause(){
        Pause?.Invoke();
    }
    void AnyInput(){
        if(Input.anyKey){
            anyInput?.Invoke();
        }
    }
    void isRun(){
            isRunning = Input.GetKey(run);
    }
    void IsInputNumber(){
        for (int i = 0; i <= 9; i++)
        {
            KeyCode key = KeyCode.Alpha0 + i;
            if (Input.GetKeyDown(key))
            {
                Debug.Log(i);
                InputNumber?.Invoke(i);
            }
        }
    }
    void OnAct(){
        Act?.Invoke();
    }
    void Update(){
        MoveInput();

        StartCrouch();
        IsCrouching();
        isRun();

        IsShoot();
        IsInputNumber();
        AnyInput();
    }

}
