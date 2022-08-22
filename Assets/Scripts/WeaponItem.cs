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

    [Header("Ammo")]
    public int currentAmmo;
    public int magazineSize;
    public int magazineSizePlusChamber;

    [Header("Reloading")]
    public float reloadTime;
    public float reloadEmptyTime;


    [Header("Sounds")]
    public AudioClip shotAudioClip;
    public AudioClip shotEmptyAudioClip;
    public AudioClip reloadAudioClip;
    public AudioClip reloadEmptyAudioClip;

    [HideInInspector] public bool isReloading;
    [HideInInspector] public bool hasShot;
    [HideInInspector] public bool hasShotOnEmpty;

}
