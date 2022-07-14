using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Weapon/Gun")]
public class GunData : ScriptableObject
{
    [Header("Info")]
    public new string name;

    [Header("Shooting")]
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
}
