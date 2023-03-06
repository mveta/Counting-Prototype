using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public static float sensitivity = 1;
    private float horizontalInput;
    private float verticalInput;
    private float rotationX;
    private float rotationY;
    [SerializeField] float limitX = 35;   
    [SerializeField] float limitY = 45;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;


    // Update is called once per frame
    void LateUpdate()
    {
        if(!pauseMenu.activeInHierarchy && !gameOverMenu.activeInHierarchy)
        {
            horizontalInput = Input.GetAxis("Mouse X");
            verticalInput = Input.GetAxis("Mouse Y");

            rotationX -= verticalInput * sensitivity;
            rotationY += horizontalInput * sensitivity;

            float x = Mathf.Clamp(rotationX, -limitX, limitX);
            float y = Mathf.Clamp(rotationY, -limitY, limitY);

            transform.eulerAngles = new Vector3(x, y, 0);
        }
    }
}
