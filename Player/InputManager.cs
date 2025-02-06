using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    [SerializeField] private KeyCode jump = KeyCode.Space;
    [SerializeField] private KeyCode slide = KeyCode.LeftControl;
    [SerializeField] private KeyCode restart = KeyCode.R;
    [SerializeField] public float x;
    [SerializeField] public float y; 
    [SerializeField] public bool isCrouching;
    [SerializeField] public bool isJumping;

    public event Action CrouchStart;
    public event Action CrouchEnd;
    public event Action Shoot;
    public event Action<int> InputNumber;
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
    void IsJumping(){
        isJumping = Input.GetKey(jump);
    }
    void IsShoot(){
        if(Input.GetMouseButton(0)){
            Shoot?.Invoke();
        }
    }
    void IsRestart(){
        if(Input.GetKeyDown(restart)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    void IsInputNumber(){
        for (int i = 0; i <= 9; i++)
        {
            KeyCode key = KeyCode.Alpha0 + i;
            if (Input.GetKeyDown(key))
            {
                InputNumber?.Invoke(i);
            }
        }
    }
    void Update(){
        MoveInput();

        StartCrouch();
        IsCrouching();
        IsJumping();
        IsRestart();

        IsShoot();
        IsInputNumber();
    }

}
