using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    AnimatorManager animatorManager;
    Animator animator;
    PlayerCamera playerCamera;
    PlayerControls playerControls;
    PlayerManager player;

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
    public bool aimInput;
    public bool aimDebug;
    public bool shootInput;
    public bool reloadInput;
    public bool interactionInput;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        animator = GetComponent<Animator>();
        playerCamera = GetComponent<PlayerCamera>();
        player = GetComponent<PlayerManager>();
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
            playerControls.PlayerActions.Aim.performed += i => aimInput = true;
            playerControls.PlayerActions.Aim.canceled += i => aimInput = false;
            playerControls.PlayerActions.Shoot.performed += i => shootInput = true;
            playerControls.PlayerActions.Shoot.canceled += i => shootInput = false;
            playerControls.PlayerActions.Reload.performed += i => reloadInput = true;
            playerControls.PlayerActions.Interact.performed += i => interactionInput = true;
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
        HandleAimInput();
        HandleShootInput();
        HandleReloadInput();
        HandleInteractionInput();
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
        if (player.isPerformingAction)
        {
            quickTurnInput = false;
            return;
        }

        if (quickTurnInput)
        {
            quickTurnInput = false;
            animator.SetBool("isPerformingQuickTurn", true);
            animatorManager.PlayAnimationWithoutRootMotion("Quick Turn", true);
            playerCamera.PerformQuickTurn();
        }
    }

    private void HandleAimInput()
    {
        if (aimInput || aimDebug)
        {
            animator.SetBool("isAiming", true);
        }
        else
        {
            animator.SetBool("isAiming", false);
        }
    }

    private void HandleShootInput()
    {
        if (shootInput)
        {
            animator.SetBool("isShooting", true);
        }
        else
        {
            animator.SetBool("isShooting", false);
        }
    }

    private void HandleReloadInput()
    {
        if (reloadInput)
        {
            reloadInput = false;
            animator.SetBool("isReloading", true);
        }
        else
        {
            animator.SetBool("isReloading", false);
        }
    }

    private void HandleInteractionInput()
    {
        if (interactionInput)
        {
            if (/*!player.canInteract || */player.isAiming)
            {
                interactionInput = false;
            }
        }
    }
}
