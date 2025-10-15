using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsScript : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropdown;

    private Resolution[] resolutions;
    private List<Resolution> uniqueResolutions = new List<Resolution>();

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        foreach (var res in resolutions)
        {
            bool duplicateFound = false;
            foreach (var unique in uniqueResolutions)
            {
                if (res.width == unique.width && res.height == unique.height)
                {
                    duplicateFound = true;
                    break;
                }
            }
            if (!duplicateFound)
                uniqueResolutions.Add(res);
        }

        int currentResolutionIndex = 0;
        for (int i = 0; i < uniqueResolutions.Count; i++)
        {
            string option = uniqueResolutions[i].width + "x" + uniqueResolutions[i].height;
            options.Add(option);

            if (uniqueResolutions[i].width == Screen.currentResolution.width &&
                uniqueResolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = uniqueResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}