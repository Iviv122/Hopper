using UnityEngine;

abstract public class Projectile : MonoBehaviour
{
    public delegate void OnDisable(Projectile Instance);
    public OnDisable Disable;
    abstract public void Spawn(Transform target);
}
