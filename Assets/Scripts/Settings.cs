using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    private bool isFullScreen = true;

    public AudioMixer am;

    Resolution[] rsl;
    List<string> resolutions;
    public Dropdown dropdown;

    public Slider sliderofvolume;
    public Dropdown dropdownofquality;
    public Dropdown dropdownofresolution;

    public void FullScreenToggle()
    {
        isFullScreen = !isFullScreen;
        Screen.fullScreen = isFullScreen;
    }

    public void AudioVolume(float sliderValue)
    {
        am.SetFloat("masterVolume", sliderValue);
        PlayerPrefs.SetFloat("Prefs_masterVolume", sliderValue);
        PlayerPrefs.Save();
    }

    public void Quality(int q)
    {
        QualitySettings.SetQualityLevel(q);
        PlayerPrefs.SetInt("Prefs_q", q);
        PlayerPrefs.Save();
    }

    public void Awake()
    {
        resolutions = new List<string>();
        rsl = Screen.resolutions;
        foreach (var i in rsl)
        {
            resolutions.Add(i.width + "x" + i.height);
        }
        dropdown.ClearOptions();
        dropdown.AddOptions(resolutions);

        //if (PlayerPrefs.HasKey("Prefs_masterVolume"))
        //{
        //    am.SetFloat("masterVolume", PlayerPrefs.GetFloat("Prefs_masterVolume"));
        //    QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Prefs_q"));
        //    Screen.SetResolution(rsl[PlayerPrefs.GetInt("Prefs_r")].width, rsl[PlayerPrefs.GetInt("Prefs_r")].height, isFullScreen);
        //}
    }

    public void Resolution(int r)
    {
        Screen.SetResolution(rsl[r].width, rsl[r].height, isFullScreen);
        PlayerPrefs.SetInt("Prefs_r", r);
        PlayerPrefs.Save();
    }

    public void Start()
    {
        if(PlayerPrefs.HasKey("Prefs_masterVolume"))
        {
            am.SetFloat("masterVolume", PlayerPrefs.GetFloat("Prefs_masterVolume"));
            sliderofvolume.SetValueWithoutNotify(PlayerPrefs.GetFloat("Prefs_masterVolume"));
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Prefs_q"));
            dropdownofquality.SetValueWithoutNotify(PlayerPrefs.GetInt("Prefs_q"));
            Screen.SetResolution(rsl[PlayerPrefs.GetInt("Prefs_r")].width, rsl[PlayerPrefs.GetInt("Prefs_r")].height, isFullScreen);
            dropdownofresolution.SetValueWithoutNotify(PlayerPrefs.GetInt("Prefs_r"));
        }
    }

    //public void Update()
    //{
    //    if (PlayerPrefs.HasKey("Prefs_masterVolume"))
    //    {
    //        am.SetFloat("masterVolume", PlayerPrefs.GetFloat("Prefs_masterVolume"));
    //        sliderofvolume.SetValueWithoutNotify(PlayerPrefs.GetFloat("Prefs_masterVolume"));
    //        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Prefs_q"));
    //        dropdownofquality.SetValueWithoutNotify(PlayerPrefs.GetInt("Prefs_q"));
    //        Screen.SetResolution(rsl[PlayerPrefs.GetInt("Prefs_r")].width, rsl[PlayerPrefs.GetInt("Prefs_r")].height, isFullScreen);
    //        dropdownofresolution.SetValueWithoutNotify(PlayerPrefs.GetInt("Prefs_r"));
    //    }
    //}
}
