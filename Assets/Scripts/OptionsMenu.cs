using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public TextMeshProUGUI sensitivityValue;
    public TextMeshProUGUI volumeValue;
    public TextMeshProUGUI musicValue;
    public Slider sensitivitySlider;
    public Slider volumeSlider;
    public Slider musicSlider;
    public AudioSource bgMusic;

    private void Awake()
    {
        bgMusic = GameObject.Find("BGMusic").GetComponent<AudioSource>();
    }
    /*
    public AudioMixer audioMixer;
      
    public void SetMainVolume(float volume)
    {
        audioMixer.SetFloat("mainVolume", volume);
    }
    public void SetSfxVolume(float volume)
    {
        audioMixer.SetFloat("sfxVolume", volume);
    }
    public void SetColVolume(float volume)
    {
        audioMixer.SetFloat("collisionVolume", volume);
    }
    */
    private void Update()
    {
        sensitivitySlider.value = CameraMovement.sensitivity;
        volumeSlider.value = AudioListener.volume;
        musicSlider.value = bgMusic.volume;
        musicValue.text = bgMusic.volume.ToString("F1");
        
    }
    public void SetMusicVolume(float vol)
    {
       
        bgMusic.volume = vol;
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeValue.SetText(volume.ToString("F1")); //one decimal

    }
    public void SetSensitivity(float sensitivity)
    {
        CameraMovement.sensitivity = sensitivity;
        sensitivityValue.SetText(sensitivity.ToString("F1"));

    }
}
