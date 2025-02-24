using System;
using UnityEngine;

public class IDeleteEvent : MonoBehaviour
{
    public event Action Destroyed;
    private void OnDestroy() {
        Destroyed?.Invoke();
    }
}
