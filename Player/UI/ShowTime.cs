using System;
using TMPro;
using UnityEngine;

public class ShowTime : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textfield;
    private float time;
    private bool isCounting = false;
    int minutes;
    int seconds;
    int milliseconds;

    void Start()
    {
        FindAnyObjectByType<InputManager>().InputRegistred += StartCounting;
        textfield.text = string.Format("{0:D2}:{1:D2}:{2:D3}", minutes, seconds, milliseconds);
    }

    void StartCounting()
    {
        isCounting = true;
    }

    void Update()
    {
        if (isCounting)
        {
            time += Time.deltaTime;
            minutes = Mathf.FloorToInt(time / 60f);
            seconds = Mathf.FloorToInt(time % 60);
            milliseconds = Mathf.FloorToInt((time % 1) * 1000);

            textfield.text = string.Format("{0:D2}:{1:D2}:{2:D3}", minutes, seconds, milliseconds);
        }
    }
}
