using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterAnimator : MonoBehaviour
{
    Animator animator;
    PlayerCameraAim playerCameraAim;
    PlayerMovement playerMovement;

    public Rig aimLayer;

    const float locomotionAnimationSmoothTime = 0.1f;

    public float aimTime = 0.3f;
    public bool isAimedIn;

    float parameterFactor;

    float speedHorizontal;
    float speedVertical;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        playerCameraAim = GetComponent<PlayerCameraAim>();
        playerMovement = GetComponent<PlayerMovement>();

        //Calculate factor to multiply with for parameters
        parameterFactor = playerMovement.walkSpeed / playerMovement.runSpeed;
    }


    void Update()
    {
        //Blend Tree for Movement
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

        //Rig Layer
        if (playerCameraAim.isAiming)
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
