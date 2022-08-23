using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimatorManager : MonoBehaviour
{
    Animator weaponAnimator;
    [SerializeField] WeaponItem weaponItem;

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
        if (weaponItem.currentAmmo == 0)
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
        if (weaponItem.currentAmmo == 0)
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

    public void ReloadWeapon()
    {
        if (weaponItem.currentAmmo == 0)
        {
            weaponAnimator.Play("Reload_Empty");
        }
        else weaponAnimator.Play("Reload");
    }
}
