using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentManager : MonoBehaviour
{
    AnimatorManager animatorManager;
    WeaponLoaderSlot weaponLoaderSlot;

    [Header("Current Equipment")]
    public WeaponItem weapon;
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
    }
}
