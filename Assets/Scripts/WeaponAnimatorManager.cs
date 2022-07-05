using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimatorManager : MonoBehaviour
{
    Animator weaponAnimator;
    [SerializeField] GunData gunData;

    [Header("Weapon FX")]
    public GameObject weaponMuzzleFlashFX;
    public GameObject weaponBulletCaseFX;

    [Header("FX Transforms")]
    public Transform weaponMuzzleFlashTransform;
    public Transform weaponBulletCaseTransform;

    private void Awake()
    {
        weaponAnimator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (gunData.currentAmmo == 0)
        {
            weaponAnimator.SetBool("Magazine_Empty", true);
        }
        else
        {
            weaponAnimator.SetBool("Magazine_Empty", false);
        }
    }

    public void ShootWeapon()
    {
        if (gunData.currentAmmo == 0)
        {
            weaponAnimator.Play("Shoot_Last_Bullet");
        }
        else weaponAnimator.Play("Shoot");

        //Instantiate Muzzle Flash FX
        GameObject muzzleFlash = Instantiate(weaponMuzzleFlashFX, weaponMuzzleFlashTransform);
        muzzleFlash.transform.parent = null;

        //Instantiate Bullet Casing
        GameObject bulletCase = Instantiate(weaponBulletCaseFX, weaponBulletCaseTransform);
        bulletCase.transform.parent = null;
    }
}
