using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI counter;
    HoopBehaviour hoopBehaviour;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameOverMenu;

    public TextMeshProUGUI blueCounter;
    public TextMeshProUGUI yellowCounter;
    public TextMeshProUGUI redCounter;
    public TextMeshProUGUI remainingHoop;
    public TextMeshProUGUI timer;
    public AudioSource shoot;
    public AudioSource gatherStrength;
    public AudioSource counterUp;
    public AudioSource gameOverSound;
    public AudioSource bgMusic;

    private int hoopCount;
    public bool isPaused;
    private float time;
 

    void Start()
    {
        hoopBehaviour = GameObject.FindGameObjectWithTag("Hoop").GetComponent<HoopBehaviour>();
        counter.SetText("Counter: 0");
        hoopCount = ObjectPool.SharedInstance.amountToPool;
        bgMusic = GameObject.Find("BGMusic").GetComponent<AudioSource>();
        Cursor.visible = false;
        isPaused = false;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !gameOverMenu.activeInHierarchy)
        {
            PauseGame();
        }
        if(!isPaused && !gameOverMenu.activeInHierarchy)
        {
            TimerUp();
        }
        
    }
    public void CounterUp()
    {
        hoopBehaviour.count++;
        counter.SetText("Counter: " + hoopBehaviour.count);
        counterUp.Play();
    }
    public void ColourCounterUp(Colour colour)
    {
        switch (colour)
        {
            case Colour.Blue:
                hoopBehaviour.blueCount++;
                break;
            case Colour.Red:
                hoopBehaviour.redCount++;
                break;
            case Colour.Yellow:
                hoopBehaviour.yellowCount++;
                break;

        }

  
    }

    public void RemainingHoopCount()
    {
        hoopCount--;
        remainingHoop.SetText("Hoops: " + hoopCount);
    }
    public void PauseGame()
    {
        
        if (Time.timeScale == 1)
        {
      
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            Cursor.visible = true;
            isPaused = true;

        }else
        {
            ResumeGame();
        }
    }

 
    public void ResumeGame()
    {
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        StartCoroutine(WaitResume());
        
        Time.timeScale = 1;
        
    }
    public void QuitToMenu()
    {
        bgMusic.Stop();
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void GameOver()
    {
        Cursor.visible = true;
        gameOverMenu.SetActive(true);
        blueCounter.SetText(hoopBehaviour.blueCount.ToString());
        yellowCounter.SetText(hoopBehaviour.yellowCount.ToString());
        redCounter.SetText(hoopBehaviour.redCount.ToString());

    }

    public void PlayShoot()
    {
        shoot.Play();
    }
    public void PlayStrength()
    {
        gatherStrength.Play();

    }
    public void StopStrength()
    {
        gatherStrength.Stop();
    }
    private IEnumerator WaitResume()
    {
        // To prevent shoot action when resume button is pressed.
        yield return new WaitForSecondsRealtime(.1f);
        isPaused = false;
    }
    public void PlayGameOver()
    {
        gameOverSound.Play();
    }
    void TimerUp()
    { 
        time += Time.deltaTime;
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    

}
