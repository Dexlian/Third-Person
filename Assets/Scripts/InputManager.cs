using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    AnimatorManager animatorManager;
    PlayerCamera playerCamera;
    PlayerControls playerControls;
    PlayerManager playerManager;

    [Header("Player Movement")]
    public float verticalMovementInput;
    public float horizontalMovementInput;
    private Vector2 movementInput;

    [Header("Camera Rotation")]
    public float verticalCameraInput;
    public float horizontalCameraInput;
    private Vector2 cameraInput;

    [Header("Button Inputs")]
    public bool runInput;
    public bool quickTurnInput;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        playerCamera = GetComponent<PlayerCamera>();
        playerManager = GetComponent<PlayerManager>();
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Run.performed += i => runInput = true;
            playerControls.PlayerMovement.QuickTurn.performed += i => quickTurnInput = true;
        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    //Called in PlayerManager
    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleCameraInput();
        HandleRunInput();
        HandleQuickTurnInput();
    }

    private void HandleMovementInput()
    {
        horizontalMovementInput = movementInput.x;
        verticalMovementInput = movementInput.y;
        animatorManager.HandleAnimatorValues(horizontalMovementInput, verticalMovementInput);
    }

    private void HandleCameraInput()
    {
        horizontalCameraInput = cameraInput.x;
        verticalCameraInput = cameraInput.y;
    }

    private void HandleRunInput()
    {
        if (runInput)
        {
            runInput = false;
        }
    }

    private void HandleQuickTurnInput()
    {
        if (playerManager.isPerformingAction)
        {
            quickTurnInput = false;
            return;
        }

        if (quickTurnInput)
        {
            quickTurnInput = false;
            animatorManager.PlayAnimationWithoutRootMotion("Quick Turn", true);
            playerCamera.PerformQuickTurn();
        }
    }
}
