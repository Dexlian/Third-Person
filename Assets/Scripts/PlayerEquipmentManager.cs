using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentManager : MonoBehaviour
{
    AnimatorManager animatorManager;
    WeaponLoaderSlot weaponLoaderSlot;

    [Header("Current Equipment")]
    public WeaponItem weapon;
    RightHandIKTarget rightHandIK;
    RightHandIKHintTarget rightHandHintIK;
    LeftHandIKTarget leftHandIK;
    LeftHandIKHintTarget leftHandHintIK;
    WeaponPivotIKTarget weaponPivotIK;


    //public SubweaponItem subWeapon;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        LoadWeaponLoaderSlots();
    }

    private void Start()
    {
        LoadCurrentWeapon();
    }

    private void LoadWeaponLoaderSlots()
    {
        //Back Slot
        //Hip Slot
        weaponLoaderSlot = GetComponentInChildren<WeaponLoaderSlot>();
    }

    private void LoadCurrentWeapon()
    {
        weaponLoaderSlot.LoadWeaponModel(weapon);
        animatorManager.animator.runtimeAnimatorController = weapon.weaponAnimator;

        rightHandIK = weaponLoaderSlot.currentWeaponModel.GetComponentInChildren<RightHandIKTarget>();
        rightHandHintIK = weaponLoaderSlot.currentWeaponModel.GetComponentInChildren<RightHandIKHintTarget>();
        leftHandIK = weaponLoaderSlot.currentWeaponModel.GetComponentInChildren<LeftHandIKTarget>();
        leftHandHintIK = weaponLoaderSlot.currentWeaponModel.GetComponentInChildren<LeftHandIKHintTarget>();
        weaponPivotIK = weaponLoaderSlot.currentWeaponModel.GetComponentInChildren<WeaponPivotIKTarget>();

        animatorManager.AssignHandIK(rightHandIK, rightHandHintIK, leftHandIK, leftHandHintIK, weaponPivotIK);
        int weaponAnimationType = ((int)weapon.weaponAnimationType);
        animatorManager.SetUpRigLayer(weaponAnimationType);
    }
}
