using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] WeaponAnimatorManager weaponAnimatorManager;
    [SerializeField] GunData gunData;
    [SerializeField] AudioManager audioManager;
    [SerializeField] Camera mainCamera;
    public Transform laserAimModule;

    [Header("Layer Mask")]
    [SerializeField] LayerMask shootableLayers;
    [SerializeField] LayerMask hitmarkerLayers;

    [Header("FX")]
    public GameObject bloodSplatterFX;
    public GameObject bulletImpactNormalFX;
    public GameObject bulletImpactMetalFX;

    private float timeSinceLastShot;

    private void Start()
    {
        PlayerShooting.shootInput += Shoot;
        PlayerShooting.reloadInput += StartReload;
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        //Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 100);
    }

    public void StartReload()
    {
        if (!gunData.isReloading && gunData.currentAmmo > 0)
        {
            StartCoroutine(Reload());
        }

        else if (!gunData.isReloading && gunData.currentAmmo == 0)
        {
            StartCoroutine(ReloadEmpty());
        }
    }

    private IEnumerator Reload()
    {
        gunData.isReloading = true;
        audioManager.Play(gunData.reloadSound.name);

        yield return new WaitForSeconds(gunData.reloadTime);

        gunData.currentAmmo = gunData.magazineSize + 1;

        gunData.isReloading = false;
    }

    private IEnumerator ReloadEmpty()
    {
        gunData.isReloading = true;
        audioManager.Play(gunData.reloadEmptySound.name);

        yield return new WaitForSeconds(gunData.reloadEmptyTime);

        gunData.currentAmmo = gunData.magazineSize;

        gunData.isReloading = false;
    }

    private bool CanShoot() => !gunData.isReloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);

    public void Shoot()
    {
        if (CanShoot() && gunData.currentAmmo > 0)
        {
            if (Physics.Raycast(laserAimModule.position, laserAimModule.forward, out RaycastHit hitInfo, gunData.maximumDistance, shootableLayers))
            {
                IDamageable damageable = hitInfo.transform.GetComponentInParent<IDamageable>();
                damageable?.TakeDamage(gunData.damage);

                IStaggerable staggerable = hitInfo.transform.GetComponentInParent<IStaggerable>();
                staggerable?.TryToStagger(gunData.staggerChance);

                ZombieEffectManager zombie = hitInfo.collider.gameObject.GetComponentInParent<ZombieEffectManager>();

                if (zombie != null)
                {
                    if (hitInfo.collider.gameObject.layer == 9)
                    {
                        zombie.DamageZombieHead();

                        GameObject bloodSplatter = Instantiate(bloodSplatterFX, hitInfo.transform);
                        bloodSplatter.transform.parent = null;
                    }
                    else if (hitInfo.collider.gameObject.layer == 10)
                    {
                        zombie.DamageZombieTorso();

                        GameObject bloodSplatter = Instantiate(bloodSplatterFX, hitInfo.transform);
                        bloodSplatter.transform.parent = null;
                    }
                    else if (hitInfo.collider.gameObject.layer == 11)
                    {
                        zombie.DamageZombieLeftArm();

                        GameObject bloodSplatter = Instantiate(bloodSplatterFX, hitInfo.transform);
                        bloodSplatter.transform.parent = null;
                    }
                    else if (hitInfo.collider.gameObject.layer == 12)
                    {
                        zombie.DamageZombieRightArm();

                        GameObject bloodSplatter = Instantiate(bloodSplatterFX, hitInfo.transform);
                        bloodSplatter.transform.parent = null;
                    }
                    else if (hitInfo.collider.gameObject.layer == 13)
                    {
                        zombie.DamageZombieLeftLeg();

                        GameObject bloodSplatter = Instantiate(bloodSplatterFX, hitInfo.transform);
                        bloodSplatter.transform.parent = null;
                    }
                    else if (hitInfo.collider.gameObject.layer == 14)
                    {
                        zombie.DamageZombieRightLeg();

                        GameObject bloodSplatter = Instantiate(bloodSplatterFX, hitInfo.transform);
                        bloodSplatter.transform.parent = null;
                    }
                }
                else if (zombie == null)
                {
                    if (hitInfo.collider.gameObject.layer == 6)
                    {
                        if (hitInfo.collider.gameObject.CompareTag("FloorNormal"))
                        {
                            GameObject bulletImpactNormal = Instantiate(bulletImpactNormalFX, hitInfo.point, Quaternion.identity);
                            bulletImpactNormal.transform.parent = null;
                        }
                        else if (hitInfo.collider.gameObject.CompareTag("FloorMetal"))
                        {
                            GameObject bulletImpactMetal = Instantiate(bulletImpactMetalFX, hitInfo.point, Quaternion.identity);
                            bulletImpactMetal.transform.parent = null;
                        }
                    }
                }
            }

            gunData.currentAmmo--;
            timeSinceLastShot = 0;
            OnGunShot();
        }

        else if (CanShoot() && gunData.currentAmmo == 0)
        {
            audioManager.Play(gunData.shotEmptySound.name);
            timeSinceLastShot = 0;
        }
    }

    private void OnGunShot()
    {
        audioManager.Play(gunData.shotSound.name);

        weaponAnimatorManager.ShootWeapon();
    }
}