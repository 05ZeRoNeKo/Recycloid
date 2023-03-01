using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Dropdown resolutionDropdown;
    Resolution[] resolutions;

    public Slider bgmUI;
    public Slider sfxUI;

    private void Start()
    {
        //get all available resolutions for the system
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        //make a list for dropdown
        List<string> options = new List<string>();

        //add each item in array to list
        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " X " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        //add list to dropdown options
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        SetAudio();
    }

    //audio settings
    public void SetBGM(float bgm)
    {
        audioMixer.SetFloat("BGMVolume", bgm);
        PlayerPrefs.SetFloat("savedBGM", bgm);
    }
    public void SetSFX(float sfx)
    {
        audioMixer.SetFloat("SFXVolume", sfx);
        PlayerPrefs.SetFloat("savedSFX", sfx);
    }
    public void SetAudio()
    {
        bgmUI.value = PlayerPrefs.GetFloat("savedBGM"); ;
        sfxUI.value = PlayerPrefs.GetFloat("savedSFX"); ;
    }
    

    //graphics quality and screen resolution settings
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
  
}
