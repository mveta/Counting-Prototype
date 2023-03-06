using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class HoopBehaviour : MonoBehaviour
{
    
    private Rigidbody hoopRb;
    private Camera cam;
    ObjectPool objectPool;
    private Slider strengthIndicator;
    private GameManager gameManager;
    public AudioSource[] collisionSounds;
    public AudioSource[] pegCollisions;
    

    Colour objColour;

    public float forceStrength;
    private float maxStrength = 2200;
    private float minStrength = 500;
    private float strengthUp = 500;

    public bool isShot;

    public int count;
    public int blueCount;
    public int yellowCount;
    public int redCount;
    public bool gameOver;

    void Start()
    {
        objectPool = ObjectPool.SharedInstance;
        cam = Camera.main;
        hoopRb = GetComponent<Rigidbody>();
        strengthIndicator = GameObject.FindGameObjectWithTag("StrengthInd").GetComponent<Slider>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        objColour = gameObject.GetComponent<ColourEnum>().objColour;
       

        isShot = false;
        count = 0;
        blueCount = 0;
        yellowCount = 0;
        redCount = 0;
        forceStrength = minStrength;
        

    }

    void Update()
    {
        // The hoop is rotated with camera so that when it is shot, force is applied in the looking direction

        if (!isShot)
        {
            transform.rotation = cam.transform.rotation;
        }

        if(Input.GetMouseButtonDown(0) && !isShot && !gameManager.isPaused)
        {
            gameManager.PlayStrength();
        }
        if (Input.GetMouseButton(0) && !isShot && !gameManager.isPaused)
        {
            GatherStrength();
        }
        else if (Input.GetMouseButtonUp(0) && !isShot && !gameManager.isPaused)
        {
            gameManager.StopStrength();
            Shoot();
            StartCoroutine(SpawnHoops());
            
        }


        //Slider
        strengthIndicator.value = forceStrength;
    }

    void Shoot()
    {
        
        hoopRb.constraints = RigidbodyConstraints.None;
        hoopRb.AddRelativeForce(Vector3.forward * forceStrength);
        isShot = true;
        forceStrength = minStrength;
        gameManager.PlayShoot();
    }
    void GatherStrength()
    {
        
        if(forceStrength < maxStrength)
        {
            forceStrength += strengthUp * Time.deltaTime;
            
        }else
        {
            forceStrength = maxStrength;
        }
        
       
    }
    private void OnTriggerEnter(Collider other)
    {
        ColourEnum otherColour = other.GetComponent<ColourEnum>();
        
        if (otherColour != null && other.CompareTag("Peg") && objColour == otherColour.objColour)
        {
            gameManager.CounterUp();

            gameManager.ColourCounterUp(objColour);

        }
      

    }

    IEnumerator SpawnHoops()
    {
        
        GameObject hoop = objectPool.GetHoop();

        

        // Get hoop from object pool, if there is none, call game over.
        if (hoop != null)
        {
            // Wait until the shot hoop is nearly settled.
            yield return new WaitForSeconds(2);
            hoop.SetActive(true);
            gameManager.RemainingHoopCount();
        }else
        {
            gameManager.bgMusic.Stop();
            gameManager.PlayGameOver();
            
            yield return new WaitForSeconds(3);
            gameOver = true;
            gameManager.GameOver();
        }
        yield return null;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            int i = Random.Range(0, collisionSounds.Length);
            collisionSounds[i].Play();

        }else if(collision.gameObject.CompareTag("Peg") || collision.gameObject.CompareTag("Hoop"))
        {
            int i = Random.Range(0, pegCollisions.Length);
            pegCollisions[i].Play();
        }
       
    }

}
