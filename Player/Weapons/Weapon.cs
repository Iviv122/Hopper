using System;
using UnityEngine;

[Serializable]
abstract public class Weapon : MonoBehaviour
{
    public int CurrentAmmo;
    abstract public void Shoot(Camera cam,Rigidbody player);
    abstract public int MaxAmmo {get;}
    abstract public GameObject Bullet {get;}
}
