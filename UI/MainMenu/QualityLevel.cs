using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class GraphicsSettingsMenu : MonoBehaviour
{
    public TMP_Dropdown qualityDropdown;

    void Start()
    {
        PopulateQualityDropdown();
        qualityDropdown.onValueChanged.AddListener(SetQuality);
    }

    void PopulateQualityDropdown()
    {
        qualityDropdown.ClearOptions();
        List<string> options = new List<string>(QualitySettings.names);
        qualityDropdown.AddOptions(options);

        // Set dropdown to current quality level
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        qualityDropdown.RefreshShownValue();
    }

    void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex, true);
    }
}