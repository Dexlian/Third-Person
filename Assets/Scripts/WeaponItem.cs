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

    [Header("Laser Aim Module")]
    public bool hasLaserAimModule;

    [Header("Shooting")]
    public bool isFullAuto;
    public float damage;
    public float staggerChance;
    public float fireRate;
    public float maximumDistance;
    public Sound shotSound;
    public Sound shotEmptySound;

    [Header("Ammo")]
    public int currentAmmo;
    public int magazineSize;
    public int magazineSizePlusChamber;

    [Header("Reloading")]
    public float reloadTime;
    public float reloadEmptyTime;
    public Sound reloadSound;
    public Sound reloadEmptySound;

    [HideInInspector] public bool isReloading;
    [HideInInspector] public bool hasShot;
    [HideInInspector] public bool hasShotOnEmpty;

}
