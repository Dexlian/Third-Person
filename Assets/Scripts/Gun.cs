using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] PlayerManager playerManager;
    [SerializeField] PlayerShooting playerShooting;
    [SerializeField] WeaponAnimatorManager weaponAnimatorManager;
    [SerializeField] GunData gunData;
    [SerializeField] AudioManager audioManager;
    [SerializeField] Camera mainCamera;
    [SerializeField] Transform laserAimModule;

    [Header("Layer Mask")]
    [SerializeField] LayerMask shootableLayers;
    [SerializeField] LayerMask hitmarkerLayers;

    [Header("FX")]
    public GameObject bloodSplatterFX;
    public GameObject bulletImpactNormalFX;
    public GameObject bulletImpactMetalFX;

    private float timeSinceLastShot;

    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }
    private void Start()
    {
        PlayerShooting.shootInput += Shoot;
        PlayerShooting.reloadInput += StartReload;

        playerManager.playerUIManager.ammoMagazineText.text = gunData.currentAmmo.ToString();
        playerManager.playerUIManager.ammoReserveText.text = playerManager.playerInventory.reservePistolAmmo.ToString();
    }

    private void OnDestroy()
    {
        PlayerShooting.shootInput -= Shoot;
        PlayerShooting.reloadInput -= StartReload;
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        if (playerShooting.playerShootingFullAuto && gunData.isFullAuto)
        {
            Shoot();
        }

        //Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 100);
    }

    public void StartReload()
    {
        if (!gunData.isReloading && gunData.currentAmmo > 0 && playerManager.playerInventory.reservePistolAmmo > 0)
        {
            StartCoroutine(Reload());
        }

        else if (!gunData.isReloading && gunData.currentAmmo == 0 && playerManager.playerInventory.reservePistolAmmo > 0)
        {
            StartCoroutine(ReloadEmpty());
        }
    }

    private IEnumerator Reload()
    {
        int bulletsToReload;

        gunData.isReloading = true;
        audioManager.Play(gunData.reloadSound.name);

        yield return new WaitForSeconds(gunData.reloadTime);

        bulletsToReload = gunData.magazineSizePlusChamber - gunData.currentAmmo;

        if (playerManager.playerInventory.reservePistolAmmo >= bulletsToReload)
        {
            gunData.currentAmmo = gunData.magazineSizePlusChamber;
            playerManager.playerInventory.reservePistolAmmo -= bulletsToReload;
        }
        else
        {
            gunData.currentAmmo += playerManager.playerInventory.reservePistolAmmo;
            playerManager.playerInventory.reservePistolAmmo = 0;
        }

        playerManager.playerUIManager.ammoMagazineText.text = gunData.currentAmmo.ToString();
        playerManager.playerUIManager.ammoReserveText.text = playerManager.playerInventory.reservePistolAmmo.ToString();
        playerManager.playerUIManager.ammoCountFade.CheckMagazine();

        gunData.isReloading = false;
    }

    private IEnumerator ReloadEmpty()
    {
        int bulletsToReload;

        gunData.isReloading = true;
        audioManager.Play(gunData.reloadEmptySound.name);

        yield return new WaitForSeconds(gunData.reloadEmptyTime);

        bulletsToReload = gunData.magazineSize;

        if (playerManager.playerInventory.reservePistolAmmo >= bulletsToReload)
        {
            gunData.currentAmmo = gunData.magazineSize;
            playerManager.playerInventory.reservePistolAmmo -= bulletsToReload;
        }
        else
        {
            gunData.currentAmmo += playerManager.playerInventory.reservePistolAmmo;
            playerManager.playerInventory.reservePistolAmmo = 0;
        }

        playerManager.playerUIManager.ammoMagazineText.text = gunData.currentAmmo.ToString();
        playerManager.playerUIManager.ammoReserveText.text = playerManager.playerInventory.reservePistolAmmo.ToString();
        playerManager.playerUIManager.ammoCountFade.CheckMagazine();

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
                IStaggerable staggerable = hitInfo.transform.GetComponentInParent<IStaggerable>();

                ZombieManager zombie = hitInfo.collider.gameObject.GetComponentInParent<ZombieManager>();
                ZombieEffectManager zombieEffect = hitInfo.collider.gameObject.GetComponentInParent<ZombieEffectManager>();


                if (zombieEffect != null)
                {
                    if (hitInfo.collider.gameObject.layer == 9)
                    {
                        zombieEffect.DamageZombieHead();
                        damageable?.TakeDamage(gunData.damage * zombie.zombieDamageMultiplierHead);
                        staggerable?.TryToStagger(1);

                        GameObject bloodSplatter = Instantiate(bloodSplatterFX, hitInfo.transform);
                        bloodSplatter.transform.parent = null;
                    }
                    else if (hitInfo.collider.gameObject.layer == 10)
                    {
                        zombieEffect.DamageZombieTorso();
                        damageable?.TakeDamage(gunData.damage * zombie.zombieDamageMultiplierTorso);
                        staggerable?.TryToStagger(gunData.staggerChance * zombie.zombieStaggerMultiplierTorso);

                        GameObject bloodSplatter = Instantiate(bloodSplatterFX, hitInfo.transform);
                        bloodSplatter.transform.parent = null;
                    }
                    else if (hitInfo.collider.gameObject.layer == 11)
                    {
                        zombieEffect.DamageZombieLeftArm();
                        damageable?.TakeDamage(gunData.damage * zombie.zombieDamageMultiplierArm);
                        staggerable?.TryToStagger(gunData.staggerChance * zombie.zombieStaggerMultiplierArm);

                        GameObject bloodSplatter = Instantiate(bloodSplatterFX, hitInfo.transform);
                        bloodSplatter.transform.parent = null;
                    }
                    else if (hitInfo.collider.gameObject.layer == 12)
                    {
                        zombieEffect.DamageZombieRightArm();
                        damageable?.TakeDamage(gunData.damage * zombie.zombieDamageMultiplierArm);
                        staggerable?.TryToStagger(gunData.staggerChance * zombie.zombieStaggerMultiplierArm);

                        GameObject bloodSplatter = Instantiate(bloodSplatterFX, hitInfo.transform);
                        bloodSplatter.transform.parent = null;
                    }
                    else if (hitInfo.collider.gameObject.layer == 13)
                    {
                        zombieEffect.DamageZombieLeftLeg();
                        damageable?.TakeDamage(gunData.damage * zombie.zombieDamageMultiplierLeg);
                        staggerable?.TryToStagger(gunData.staggerChance * zombie.zombieStaggerMultiplierLeg);

                        GameObject bloodSplatter = Instantiate(bloodSplatterFX, hitInfo.transform);
                        bloodSplatter.transform.parent = null;
                    }
                    else if (hitInfo.collider.gameObject.layer == 14)
                    {
                        zombieEffect.DamageZombieRightLeg();
                        damageable?.TakeDamage(gunData.damage * zombie.zombieDamageMultiplierLeg);
                        staggerable?.TryToStagger(gunData.staggerChance * zombie.zombieStaggerMultiplierLeg);

                        GameObject bloodSplatter = Instantiate(bloodSplatterFX, hitInfo.transform);
                        bloodSplatter.transform.parent = null;
                    }
                }
                else if (zombieEffect == null)
                {
                    damageable?.TakeDamage(gunData.damage);

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
            playerManager.playerUIManager.ammoMagazineText.text = gunData.currentAmmo.ToString();
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