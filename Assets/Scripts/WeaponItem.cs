using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

[CreateAssetMenu(menuName = "Items/Weapon Item")]
public class WeaponItem : Item
{
    [Header("Weapon Animation")]
    public AnimatorOverrideController weaponAnimator;
    public enum WeaponAnimationType
    {
        Pistol,
        SubmachineGun,
        Rifle
    }

    public WeaponAnimationType weaponAnimationType;
    
}
