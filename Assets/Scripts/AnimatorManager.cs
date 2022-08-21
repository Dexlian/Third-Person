using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AnimatorManager : MonoBehaviour
{
    public Animator animator;
    public TwoBoneIKConstraint rightHandIK;
    public TwoBoneIKConstraint leftHandIK;

    RigBuilder rigBuilder;
    PlayerCamera playerCamera;
    PlayerMovement playerMovement;

    public Rig idleLayer;
    public RigLayerWeaponPoseIdlePistol rigLayerWeaponPoseIdlePistol;

    public MultiPositionConstraint idleMultiPositionConstraint;
    public MultiParentConstraint idleMultiParentConstraint;
    public MultiRotationConstraint idleMultiRotationConstraint;

    public Rig aimLayer;

    const float locomotionAnimationSmoothTime = 0.1f;

    public float aimTime = 0.3f;
    public bool isAimedIn;

    float snappedHorizontal;
    float snappedVertical;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rigBuilder = GetComponent<RigBuilder>();
        playerCamera = GetComponent<PlayerCamera>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        HandleRigLayer();
    }

    public void PlayAnimationWithoutRootMotion(string targetAnimation, bool isPerformingAction)
    {
        animator.SetBool("isPerformingAction", isPerformingAction);
        animator.applyRootMotion = false;
        animator.CrossFade(targetAnimation, 0.2f);
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

        animator.SetFloat("speedHorizontal", snappedHorizontal, locomotionAnimationSmoothTime, Time.deltaTime);
        animator.SetFloat("speedVertical", snappedVertical, locomotionAnimationSmoothTime, Time.deltaTime);
    }

    public void AssignHandIK(RightHandIKTarget rightTarget, RightHandIKHintTarget rightHintTarget, LeftHandIKTarget leftTarget, LeftHandIKHintTarget leftHintTarget, WeaponPivotIKTarget weaponPivot)
    {
        rightHandIK.data.target = rightTarget.transform;
        rightHandIK.data.hint = rightHintTarget.transform;
        leftHandIK.data.target = leftTarget.transform;
        leftHandIK.data.hint = leftHintTarget.transform;
        idleMultiParentConstraint.data.constrainedObject = weaponPivot.transform;
        rigBuilder.Build();
    }

    public void SetUpRigLayer(int weaponAnimationType)
    {
        switch (weaponAnimationType)
        {
            //Pistol == 0
            case 0:
                //
                break;
            //SubmachineGun == 1
            case 1:
                //
                break;
            //Rifle == 2
            case 2:
                //
                break;
            default:
                break;
        }
    }

    private void HandleRigLayer()
    {
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
