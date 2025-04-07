using System;
using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    [SerializeField]static public float SensX { get; private set; } = 4;
    [SerializeField] static public float SensY { get; private set; } = 4;

    private const string SensXKey = "SensitivityX";
    private const string SensYKey = "SensitivityY";
    static public event Action updateValues;

    private void Awake()
    {
        LoadSettings();
    }

    public static void SetSensitivity(float x,float y)
    {
        SensX = x;
        SensY = y;
        SaveSettings();
    }
    private static void SaveSettings()
    {
        PlayerPrefs.SetFloat(SensXKey, SensX);
        PlayerPrefs.SetFloat(SensYKey, SensY);
        PlayerPrefs.Save();
        updateValues?.Invoke();
    }

    private void LoadSettings()
    {
        SensX = PlayerPrefs.HasKey(SensXKey) ? PlayerPrefs.GetFloat(SensXKey) : SensX;
        SensY = PlayerPrefs.HasKey(SensYKey) ? PlayerPrefs.GetFloat(SensYKey) : SensY;
    }
}
