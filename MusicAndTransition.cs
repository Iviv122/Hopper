using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneMusicManager : MonoBehaviour
{
    public static SceneMusicManager Instance;
    public AudioSource musicSource;
    public float fadeDuration = 1.5f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (musicSource == null)
            musicSource = GetComponent<AudioSource>();

        if (musicSource != null && !musicSource.isPlaying)
            musicSource.Play();
    }

    
}
