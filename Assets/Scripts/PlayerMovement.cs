using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    InputManager inputManager;
    PlayerManager playerManager;
    Rigidbody rb;

    float horizontalInput;
    float verticalInput;

    public Transform orientation;

    [Header("Movement")]
    public float moveSpeed;
    public float moveSpeedMultiplier = 10f;
    public float walkSpeed = 3f;
    public float walkSpeedBack = 1.5f;
    public float aimSpeed = 1.5f;
    public float runSpeed = 5f;
    public float maximumSpeed = 5f;
    public bool isRunning;

    [Header("Drag/Gravity")]
    public float groundDrag;
    public float extraGravity = 10f;

    public Vector3 moveDirection;
    Vector3 slopeMoveDirection;

    [Header("Ground Check")]
    public float playerHeight;
    public float groundCheckRadius = 0.5f;
    public Transform groundCheckPosition;
    public LayerMask ground;
    public bool isGrounded;

    [Header("Movement States")]
    public MovementState movementState;
    public enum MovementState
    {
        walking,
        walkingAiming,
        walkingBack,
        running,
    }

    RaycastHit slopeHit;
    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, (playerHeight * 0.5f) + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        return false;
    }

    void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerManager = GetComponent<PlayerManager>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnGUI()
    {
        GUILayout.Label("Speed: " + new Vector3(rb.velocity.x, 0, rb.velocity.z).magnitude);
        GUILayout.Label("SpeedVertical: " + rb.velocity.y);
        GUILayout.Label("X: " + rb.velocity.x);
        GUILayout.Label("Z: " + rb.velocity.z);
    }

    void Update()
    {
        //ground check
        //isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);
        isGrounded = Physics.CheckSphere(groundCheckPosition.position, groundCheckRadius, ground);

        PlayerInput();
        SpeedControl();
        StateHandler();

        //Slope handling
        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
        if (isGrounded && OnSlope()
            && (horizontalInput <= 0.1f && horizontalInput >= -0.1f)
            && (verticalInput <= 0.1f && verticalInput >= -0.1f)
            && (rb.velocity.y <= 1 && rb.velocity.y >= -1))
        {
            rb.useGravity = false;
        }
        else
        {
            rb.useGravity = true;
        }

        //handle drag
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void PlayerInput()
    {
        //walking
        if (!isRunning)
        {
            horizontalInput = inputManager.horizontalMovementInput;
        }
        verticalInput = inputManager.verticalMovementInput;

        //running
        if (verticalInput > 0f && !isRunning && inputManager.runInput)
        {
            isRunning = true;
        }

        else if (verticalInput <= 0f || (isRunning && inputManager.runInput) || playerManager.isAiming)
        {
            isRunning = false;
        }
    }

    void Movement()
    {
        if (!isRunning)
        {
            moveDirection = horizontalInput * orientation.right + verticalInput * orientation.forward;
        }
        else
        {
            moveDirection = verticalInput * orientation.forward;
        }


        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveSpeed * moveSpeedMultiplier * moveDirection.normalized, ForceMode.Force);
        }
        else if (isGrounded && OnSlope())
        {
            rb.AddForce(moveSpeed * moveSpeedMultiplier * slopeMoveDirection.normalized, ForceMode.Force);
        }

        //Add more gravity
        if (!isGrounded)
        {
            rb.AddForce(extraGravity * Vector3.down, ForceMode.Force);
        }
    }

    void SpeedControl()
    {
        //limiting speed on slope
        if (OnSlope())
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }

        else
        {
            Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            //limit velocity if needed
            if (flatVelocity.magnitude > moveSpeed)
            {
                Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
            }
        }
    }

    void StateHandler()
    {
        //Mode - Walking
        if (isGrounded && !playerManager.isAiming && !isRunning && verticalInput >= 0f)
        {
            movementState = MovementState.walking;
            moveSpeed = walkSpeed;
        }

        //Mode - Walking Aiming
        else if (isGrounded && playerManager.isAiming)
        {
            movementState = MovementState.walkingAiming;
            moveSpeed = aimSpeed;
        }

        //Mode - Walking Back
        else if (isGrounded && !playerManager.isAiming && !isRunning && verticalInput < 0f)
        {
            movementState = MovementState.walkingBack;
            moveSpeed = walkSpeedBack;
        }

        //Mode - Running
        else if (isGrounded && isRunning)
        {
            movementState = MovementState.running;
            moveSpeed = runSpeed;
        }
    }
}
