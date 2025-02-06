using System;
using UnityEngine;

public class Levitate : MonoBehaviour
{
    [SerializeField] float waveScale;
    [SerializeField] float speed;
    [SerializeField] float a=0;
    private Vector3 addVec;
    void OnEnable() {
    addVec = transform.position;     
    }
    void FixedUpdate(){
        addVec.y += Mathf.Sin(a)*waveScale;
        transform.position = addVec;
        a+=speed;
    }
}
