using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Unity Stuff")]
    InputManager inputManager;
    PlayerManager playerManager;
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

    [Header("Quick Turn")]
    public float quickTurnSmooth = 1f;
    public float quickTurnTime = 0f;
    //quick turn amount of 13.5f is almost 180 degrees
    public float quickTurnAmount = 6.792f;
    Quaternion targetRotation;

    [Header("Aiming")]
    public float sensitivityXAiming;
    public float sensitivityYAiming;
    public bool isAiming;
    public bool aimDebug;

    float mouseX;
    float mouseY;

    float xRotation;
    float yRotation;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerManager = GetComponent<PlayerManager>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        PlayerInput();

        //rotate player object
        gameObject.transform.rotation = Quaternion.Euler(0, yRotation, 0);

        //rotate camera root
        playerCameraRoot.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

        //Aiming
        if (playerManager.isAiming)
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

    private void FixedUpdate()
    {
        //Quick Turn
        quickTurnTime += Time.deltaTime;
        if (playerManager.isPerformingQuickTurn)
        {
            yRotation = Mathf.Lerp(yRotation, yRotation + quickTurnAmount, quickTurnTime);
        }

        if (quickTurnTime > 1f)
        {
            playerManager.isPerformingQuickTurn = false;
        }
    }

    void PlayerInput()
    {
        mouseX = inputManager.horizontalCameraInput;
        mouseY = inputManager.verticalCameraInput;

        yRotation += mouseX * sensitivityX * sensitivityMultiplier;
        xRotation -= mouseY * sensitivityY * sensitivityMultiplier;

        xRotation = Mathf.Clamp(xRotation, minXRotation, maxXRotation);
    }

    public void PerformQuickTurn()
    {
        quickTurnTime = 0f;
        playerManager.isPerformingQuickTurn = true;

        //targetRotation = Vector3.zero;
        //targetRotation.y = Quaternion.Euler(0, yRotation + 180, 0);

        //gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, targetRotation, quickTurnSmooth * Time.deltaTime);
        //playerCameraRoot.transform.rotation = Quaternion.RotateTowards(playerCameraRoot.transform.rotation, targetRotation, quickTurnSmooth * Time.deltaTime);

        //float targetYRotation = yRotation + 180;
        //yRotation += 180;

        //targetRotation = Quaternion.Euler(0, yRotation + 180, 0);
        //targetRotation = Quaternion.Slerp(gameObject.transform.rotation, targetRotation, quickTurnSmooth);
        //gameObject.transform.rotation = targetRotation;
        //playerCameraRoot.transform.rotation = targetRotation;
    }
}
