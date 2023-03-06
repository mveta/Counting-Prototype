using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource click;
    public AudioSource bgMusic;

    private void Start()
    {
        bgMusic = GameObject.Find("BGMusic").GetComponent<AudioSource>();
        bgMusic.Play();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
   
}
