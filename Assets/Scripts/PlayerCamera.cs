using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Unity Stuff")]
    InputManager inputManager;
    PlayerMovement playerMovement;
    public GameObject cameraNormal;
    public GameObject cameraRunning;
    public GameObject cameraAiming;
    public GameObject crosshair;
    public Transform playerCameraRoot;

    [Header("Move Camera")]
    public float sensitivityX;
    public float sensitivityY;
    public float sensitivityXNormal;
    public float sensitivityYNormal;
    public float sensitivityMultiplier = 0.01f;

    public float minXRotation = -80f;
    public float maxXRotation = 80f;

    [Header("Aiming")]
    public KeyCode aimKey = KeyCode.Mouse1;
    public float sensitivityXAiming;
    public float sensitivityYAiming;
    public bool isAiming;

    float mouseX;
    float mouseY;

    float xRotation;
    float yRotation;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        isAiming = false;
    }

    void Update()
    {
        PlayerInput();

        //rotate player object
        gameObject.transform.rotation = Quaternion.Euler(0, yRotation, 0);

        //rotate camera root
        playerCameraRoot.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        
        //Aiming
        if (isAiming)
        {
            sensitivityX = sensitivityXAiming;
            sensitivityY = sensitivityYAiming;

            crosshair.SetActive(false);
        }
        else
        {
            sensitivityX = sensitivityXNormal;
            sensitivityY = sensitivityYNormal;

            crosshair.SetActive(true);
        }

        //Cinemachine Handling
        if (playerMovement.movementState == PlayerMovement.MovementState.walking
            || playerMovement.movementState == PlayerMovement.MovementState.walkingBack)
        {
            cameraAiming.SetActive(false);
            cameraNormal.SetActive(true);
            cameraRunning.SetActive(false);
        }
        else if (playerMovement.movementState == PlayerMovement.MovementState.running)
        {
            cameraAiming.SetActive(false);
            cameraNormal.SetActive(false);
            cameraRunning.SetActive(true);
        }
        else if (playerMovement.movementState == PlayerMovement.MovementState.walkingAiming)
        {
            cameraAiming.SetActive(true);
            cameraNormal.SetActive(false);
            cameraRunning.SetActive(false);
        }
    }

    void PlayerInput()
    {
        mouseX = inputManager.horizontalCameraInput;
        mouseY = inputManager.verticalCameraInput;

        yRotation += mouseX * sensitivityX * sensitivityMultiplier;
        xRotation -= mouseY * sensitivityY * sensitivityMultiplier;

        xRotation = Mathf.Clamp(xRotation, minXRotation, maxXRotation);

        //Aiming
        if (Input.GetKey(aimKey))
        {
            isAiming = true;
        }
        else
        {
            isAiming = false;
        }
    }
}
