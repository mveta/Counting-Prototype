using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGAudio : MonoBehaviour
{
   
    private AudioSource click;
    private AudioSource bgMusic;

    void Awake()
    {
        GameObject[] music = GameObject.FindGameObjectsWithTag("Music");
        if(music.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(gameObject);

        bgMusic = music[0].GetComponent<AudioSource>();
    }
    
    public void PlayClick()
    {
        click = gameObject.GetComponent<AudioSource>();
        click.Play();
        StartCoroutine(DestroyIt());
    }
    IEnumerator DestroyIt()
    {
        yield return new WaitForSecondsRealtime(1);
        Destroy(gameObject);
        yield return null;
    }
    public void PlayBG()
    {
        bgMusic.Play();
    }

}
