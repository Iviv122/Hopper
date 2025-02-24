using System.Collections;
using Unity.VisualScripting;
using UnityEditor.SceneTemplate;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    public float growthSpeed = 1f;   // Speed at which the sphere grows
    public float maxSize = 5f;       // Maximum size of the sphere
    public float duration = 0.3f;      // Duration of the explosion

    private float currentSize = 0f;  // Current size of the sphere
    private float startTime;         // Time when the effect starts

    [SerializeField]private Material mat;
    public Color startColor;
    public Color endColor; 

    void Awake()
    {
        startTime = Time.time;    
    }
    void Update()
    {
        float elapsedTime = Time.time - startTime;  // Calculate elapsed time

        // If the elapsed time is less than the duration, increase the size
        if (elapsedTime < duration)
        {
            currentSize = Mathf.Lerp(0, maxSize, elapsedTime / duration);  // Gradually increase size
            transform.localScale = new Vector3(currentSize, currentSize, currentSize);  // Apply size to the sphere

            
            mat.color = Color.Lerp(startColor,endColor,elapsedTime/duration);
            
        }
        else
        {
            Destroy(gameObject);  // Destroy the object after the effect finishes
        }
    }
}


