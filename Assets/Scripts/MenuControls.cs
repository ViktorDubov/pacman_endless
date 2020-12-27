using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuControls : MonoBehaviour
{
    private bool isFullScreen = true;
    Resolution[] rsl;
    List<string> resolutions;
    public Dropdown dropdown;
    public AudioMixer am;
    
    public void Start()
    {
        if (PlayerPrefs.HasKey("Prefs_masterVolume"))
        {
            am.SetFloat("masterVolume", PlayerPrefs.GetFloat("Prefs_masterVolume"));
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Prefs_q"));
            Screen.SetResolution(rsl[PlayerPrefs.GetInt("Prefs_r")].width, rsl[PlayerPrefs.GetInt("Prefs_r")].height, isFullScreen);            
        }
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
    }

    public void PlayPressed()
    {
        SceneManager.LoadScene("level1");
    }

    public void ExitPressed()
    {
        Application.Quit();
        Debug.Log("Exit pressed!");
    }
}
