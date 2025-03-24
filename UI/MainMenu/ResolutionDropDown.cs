using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionDropDown : MonoBehaviour
{
    public TMP_Dropdown ResDropDown;
    public Toggle IsFullScreenToggle;
    
    bool IsFullScreen;
    Resolution[] AllResolutions;
    int SelectedResolution;
    List<Resolution> SelectedResolutionList = new List<Resolution>();

    private void Awake() {
        IsFullScreen = Screen.fullScreen;
        IsFullScreenToggle.isOn = IsFullScreen;

        AllResolutions = Screen.resolutions;
        List<string> resolutionStringList = new List<string>();
        string newRes;
        
        int defaultResolutionIndex = 0; // Store the default resolution index

        for (int i = 0; i < AllResolutions.Length; i++)
        {
            Resolution res = AllResolutions[i];
            newRes = res.width + "x" + res.height;
            if (!resolutionStringList.Contains(newRes))
            {
                resolutionStringList.Add(newRes);
                SelectedResolutionList.Add(res);

                // Check if this resolution matches the current screen resolution
                if (res.width == Screen.currentResolution.width && res.height == Screen.currentResolution.height)
                {
                    defaultResolutionIndex = SelectedResolutionList.Count - 1;
                }
            }
        }

        ResDropDown.AddOptions(resolutionStringList);
        ResDropDown.value = defaultResolutionIndex; // Set default resolution
        SelectedResolution = defaultResolutionIndex;
        ResDropDown.RefreshShownValue(); // Update dropdown display
    }

    public void ChangeResolution()
    {
        SelectedResolution = ResDropDown.value;
        Screen.SetResolution(
            SelectedResolutionList[SelectedResolution].width,
            SelectedResolutionList[SelectedResolution].height,
            IsFullScreen
        );
    }

    public void ChangeFullScreen()
    {
        IsFullScreen = IsFullScreenToggle.isOn;
        Screen.SetResolution(
            SelectedResolutionList[SelectedResolution].width,
            SelectedResolutionList[SelectedResolution].height,
            IsFullScreen
        );
    }
}
