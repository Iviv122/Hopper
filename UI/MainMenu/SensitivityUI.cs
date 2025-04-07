using UnityEngine;
using UnityEngine.UI;

public class SensitivityUI : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        // Set sliders to match saved values
        slider.value = PlayerSettings.SensX;
        slider.value = PlayerSettings.SensY;

        // Add event listeners to update settings
        slider.onValueChanged.AddListener(UpdateSensitivityX);
    }

    private void UpdateSensitivityX(float value)
    {
         
        PlayerSettings.SetSensitivity(slider.value, slider.value);
        Debug.Log(PlayerSettings.SensX + " " + PlayerSettings.SensY);
    }
}

