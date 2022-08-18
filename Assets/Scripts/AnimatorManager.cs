using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AnimatorManager : MonoBehaviour
{
    Animator animator;
    PlayerCamera playerCamera;
    PlayerMovement playerMovement;

    public Rig aimLayer;

    const float locomotionAnimationSmoothTime = 0.1f;

    public float aimTime = 0.3f;
    public bool isAimedIn;

    float parameterFactor;

    float snappedHorizontal;
    float snappedVertical;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerCamera = GetComponent<PlayerCamera>();
        playerMovement = GetComponent<PlayerMovement>();

        //Calculate factor to multiply with for parameters
        parameterFactor = playerMovement.walkSpeed / playerMovement.runSpeed;
    }

    //Called in PlayerManager
    public void HandleAnimatorValues(float horizontalMovement, float verticalMovement)
    {
        if (horizontalMovement > 0 && !playerMovement.isRunning)
        {
            snappedHorizontal = 0.5f;
        }
        else if (horizontalMovement < 0 && !playerMovement.isRunning)
        {
            snappedHorizontal = -0.5f;
        }
        else if (playerMovement.isRunning)
        {
            snappedHorizontal = 0f;
        }
        else
        {
            snappedHorizontal = 0f;
        }

        if (verticalMovement > 0 && !playerMovement.isRunning)
        {
            snappedVertical = 0.5f;
        }
        else if (verticalMovement < 0 && !playerMovement.isRunning)
        {
            snappedVertical = -0.5f;
        }
        else if (verticalMovement > 0 && playerMovement.isRunning)
        {
            snappedVertical = 1f;
        }
        else
        {
            snappedVertical = 0f;
        }

        /*
        if (!playerMovement.isRunning)
        {
            speedHorizontal = horizontalMovement * parameterFactor;
            speedVertical = verticalMovement * parameterFactor;
        }
        else
        {
            speedHorizontal = 0f;
            speedVertical = verticalMovement;
        }
        */

        animator.SetFloat("speedHorizontal", snappedHorizontal, locomotionAnimationSmoothTime, Time.deltaTime);
        animator.SetFloat("speedVertical", snappedVertical, locomotionAnimationSmoothTime, Time.deltaTime);
    }

    void Update()
    {
        /*//Blend Tree for Movement
        if (!playerMovement.isRunning)
        {
            speedHorizontal = Input.GetAxisRaw("Horizontal") * parameterFactor;
            speedVertical = Input.GetAxisRaw("Vertical") * parameterFactor;
        }
        else
        {
            speedHorizontal = 0f;
            speedVertical = Input.GetAxisRaw("Vertical");
        }

        animator.SetFloat("speedHorizontal", speedHorizontal, locomotionAnimationSmoothTime, Time.deltaTime);
        animator.SetFloat("speedVertical", speedVertical, locomotionAnimationSmoothTime, Time.deltaTime);
        */

        //Rig Layer
        if (playerCamera.isAiming)
        {
            aimLayer.weight += Time.deltaTime / aimTime;
        }
        else
        {
            aimLayer.weight -= Time.deltaTime / aimTime;
        }

        //Has aimed in?
        if (aimLayer.weight == 1)
        {
            isAimedIn = true;
        }
        else
        {
            isAimedIn = false;
        }
    }
}
