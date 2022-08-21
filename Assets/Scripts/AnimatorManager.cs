using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AnimatorManager : MonoBehaviour
{
    RigBuilder rigBuilder;
    PlayerManager playerManager;
    PlayerMovement playerMovement;

    [Header("Animator")]
    public Animator animator;

    [Header("Hand IK")]
    public TwoBoneIKConstraint rightHandIK;
    public TwoBoneIKConstraint leftHandIK;

    [Header("Layers")]
    public Rig idlePistolLayer;
    public Rig aimPistolLayer;
    public Rig idleSMGLayer;
    public Rig aimSMGLayer;
    public Rig idleRifleLayer;
    public Rig aimRifleLayer;

    [Header("Components")]
    public MultiParentConstraint idlePistolMultiParentConstraint;
    public MultiParentConstraint aimPistolMultiParentConstraint;
    public MultiParentConstraint idleSMGMultiParentConstraint;
    public MultiParentConstraint aimSMGMultiParentConstraint;
    public MultiParentConstraint idleRifleMultiParentConstraint;
    public MultiParentConstraint aimRifleMultiParentConstraint;

    [Header("Aim Time")]
    public float aimTime = 0.3f;

    public int currentAnimationType;

    const float locomotionAnimationSmoothTime = 0.1f;

    float snappedHorizontal;
    float snappedVertical;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rigBuilder = GetComponent<RigBuilder>();
        playerManager = GetComponent<PlayerManager>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        HandleRigLayer(currentAnimationType);
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

    public void AssignHandIK(RightHandIKTarget rightTarget, RightHandIKHintTarget rightHintTarget,
                             LeftHandIKTarget leftTarget, LeftHandIKHintTarget leftHintTarget,
                             WeaponPivotIKTarget weaponPivot)
    {
        rightHandIK.data.target = rightTarget.transform;
        rightHandIK.data.hint = rightHintTarget.transform;
        leftHandIK.data.target = leftTarget.transform;
        leftHandIK.data.hint = leftHintTarget.transform;
        idlePistolMultiParentConstraint.data.constrainedObject = weaponPivot.transform;
        aimPistolMultiParentConstraint.data.constrainedObject = weaponPivot.transform;
        idleSMGMultiParentConstraint.data.constrainedObject = weaponPivot.transform;
        aimSMGMultiParentConstraint.data.constrainedObject = weaponPivot.transform;
        idleRifleMultiParentConstraint.data.constrainedObject = weaponPivot.transform;
        aimRifleMultiParentConstraint.data.constrainedObject = weaponPivot.transform;
        rigBuilder.Build();
    }

    public void SetUpRigLayer(int weaponAnimationType)
    {
        switch (weaponAnimationType)
        {
            //Pistol == 0
            case 0:
                currentAnimationType = 0;
                idlePistolLayer.weight = 1;
                idleSMGLayer.weight = 0;
                idleRifleLayer.weight = 0;
                break;
            //SubmachineGun == 1
            case 1:
                currentAnimationType = 1;
                idlePistolLayer.weight = 0;
                idleSMGLayer.weight = 1;
                idleRifleLayer.weight = 0;
                break;
            //Rifle == 2
            case 2:
                currentAnimationType = 2;
                idlePistolLayer.weight = 0;
                idleSMGLayer.weight = 0;
                idleRifleLayer.weight = 1;
                break;
            default:
                break;
        }
    }

    private void HandleRigLayer(int currentAnimationType)
    {
        if (currentAnimationType == 0)
        {
            if (playerManager.isAiming)
            {
                aimPistolLayer.weight += Time.deltaTime / aimTime;
            }
            else
            {
                aimPistolLayer.weight -= Time.deltaTime / aimTime;
            }

            //Has aimed in?
            if (aimPistolLayer.weight == 1)
            {
                animator.SetBool("isAimedIn", true);
            }
            else
            {
                animator.SetBool("isAimedIn", false);
            }
        }

        if (currentAnimationType == 1)
        {
            if (playerManager.isAiming)
            {
                aimSMGLayer.weight += Time.deltaTime / aimTime;
            }
            else
            {
                aimSMGLayer.weight -= Time.deltaTime / aimTime;
            }

            //Has aimed in?
            if (aimSMGLayer.weight == 1)
            {
                animator.SetBool("isAimedIn", true);
            }
            else
            {
                animator.SetBool("isAimedIn", false);
            }
        }

        if (currentAnimationType == 2)
        {
            if (playerManager.isAiming)
            {
                aimRifleLayer.weight += Time.deltaTime / aimTime;
            }
            else
            {
                aimRifleLayer.weight -= Time.deltaTime / aimTime;
            }

            //Has aimed in?
            if (aimRifleLayer.weight == 1)
            {
                animator.SetBool("isAimedIn", true);
            }
            else
            {
                animator.SetBool("isAimedIn", false);
            }
        }
    }
}
