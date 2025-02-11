using System;
using UnityEngine;

[Serializable]
abstract public class Weapon : MonoBehaviour
{
    public int CurrentAmmo;
    public Camera cam;
    public Rigidbody rb;
    public GameObject Bullet;
    public void Initialize(Camera _cam,Rigidbody _rb){
        this.cam = _cam;
        this.rb = _rb;
    }
    abstract public void Shoot();
    abstract public int MaxAmmo {get;}
}