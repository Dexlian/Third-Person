using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] PlayerManager playerManager;
    [SerializeField] WeaponAnimatorManager weaponAnimatorManager;
    [SerializeField] WeaponItem weaponItem;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Transform weaponMuzzle;

    [Header("Layer Mask")]
    [SerializeField] LayerMask shootableLayers;
    [SerializeField] LayerMask hitmarkerLayers;

    [Header("FX")]
    public GameObject bloodSplatterFX;
    public GameObject bulletImpactNormalFX;
    public GameObject bulletImpactMetalFX;

    [Header("SFX")]
    AudioClip shotAudioClip;
    AudioClip shotEmptyAudioClip;
    AudioClip reloadAudioClip;
    AudioClip reloadEmptyAudioClip;

    private float timeSinceLastShot;

    private void Awake()
    {
        playerManager = GetComponentInParent<PlayerManager>();
        weaponAnimatorManager = GetComponent<WeaponAnimatorManager>();
        audioSource = GetComponent<AudioSource>();

    }
    private void Start()
    {
        shotAudioClip = weaponItem.shotAudioClip;
        shotEmptyAudioClip = weaponItem.shotEmptyAudioClip;
        reloadAudioClip = weaponItem.reloadAudioClip;
        reloadEmptyAudioClip = weaponItem.reloadEmptyAudioClip;

        weaponItem.isReloading = false;
        weaponItem.hasShot = false;
        weaponItem.hasShotOnEmpty = false;

        playerManager.playerUIManager.ammoMagazineText.text = weaponItem.currentAmmo.ToString();
        playerManager.playerUIManager.ammoReserveText.text = playerManager.playerInventory.reservePistolAmmo.ToString();
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        CheckForShootInput();
        ResetBoolHasShot();

        CheckForReloadInput();
    }

    private void CheckForShootInput()
    {
        //Semi-Automatic
        if (!weaponItem.isFullAuto && !weaponItem.hasShot && playerManager.isAimedIn && playerManager.isShooting)
        {
            weaponItem.hasShot = true;
            Shoot();
        }

        //Full-Auto
        if (weaponItem.isFullAuto && playerManager.isAimedIn && playerManager.isShooting && !weaponItem.hasShotOnEmpty)
        {
            Shoot();
        }
    }

    private void ResetBoolHasShot()
    {
        if (!playerManager.isShooting)
        {
            weaponItem.hasShot = false;
            weaponItem.hasShotOnEmpty = false;
        }
    }

    private void CheckForReloadInput()
    {
        if (!weaponItem.isReloading && playerManager.isAimedIn && playerManager.isReloading)
        {
            StartReload();
        }
    }

    public void StartReload()
    {
        if (!weaponItem.isReloading && weaponItem.currentAmmo > 0 && playerManager.playerInventory.reservePistolAmmo > 0)
        {
            StartCoroutine(Reload());
        }

        else if (!weaponItem.isReloading && weaponItem.currentAmmo == 0 && playerManager.playerInventory.reservePistolAmmo > 0)
        {
            StartCoroutine(ReloadEmpty());
        }
    }

    private IEnumerator Reload()
    {
        int bulletsToReload;

        playerManager.isReloading = false;
        weaponItem.isReloading = true;

        audioSource.PlayOneShot(reloadAudioClip);

        weaponAnimatorManager.ReloadWeapon();

        yield return new WaitForSeconds(weaponItem.reloadTime);

        bulletsToReload = weaponItem.magazineSizePlusChamber - weaponItem.currentAmmo;

        if (playerManager.playerInventory.reservePistolAmmo >= bulletsToReload)
        {
            weaponItem.currentAmmo = weaponItem.magazineSizePlusChamber;
            playerManager.playerInventory.reservePistolAmmo -= bulletsToReload;
        }
        else
        {
            weaponItem.currentAmmo += playerManager.playerInventory.reservePistolAmmo;
            playerManager.playerInventory.reservePistolAmmo = 0;
        }

        playerManager.playerUIManager.ammoMagazineText.text = weaponItem.currentAmmo.ToString();
        playerManager.playerUIManager.ammoReserveText.text = playerManager.playerInventory.reservePistolAmmo.ToString();
        playerManager.playerUIManager.ammoCountFade.CheckMagazine();

        weaponItem.isReloading = false;
    }

    private IEnumerator ReloadEmpty()
    {
        int bulletsToReload;

        playerManager.isReloading = false;
        weaponItem.isReloading = true;

        audioSource.PlayOneShot(reloadEmptyAudioClip);

        weaponAnimatorManager.ReloadWeapon();

        yield return new WaitForSeconds(weaponItem.reloadEmptyTime);

        bulletsToReload = weaponItem.magazineSize;

        if (playerManager.playerInventory.reservePistolAmmo >= bulletsToReload)
        {
            weaponItem.currentAmmo = weaponItem.magazineSize;
            playerManager.playerInventory.reservePistolAmmo -= bulletsToReload;
        }
        else
        {
            weaponItem.currentAmmo += playerManager.playerInventory.reservePistolAmmo;
            playerManager.playerInventory.reservePistolAmmo = 0;
        }

        playerManager.playerUIManager.ammoMagazineText.text = weaponItem.currentAmmo.ToString();
        playerManager.playerUIManager.ammoReserveText.text = playerManager.playerInventory.reservePistolAmmo.ToString();
        playerManager.playerUIManager.ammoCountFade.CheckMagazine();

        weaponItem.isReloading = false;
    }

    private bool CanShoot() => !weaponItem.isReloading && timeSinceLastShot > 1f / (weaponItem.fireRate / 60f);

    public void Shoot()
    {
        if (CanShoot() && weaponItem.currentAmmo > 0)
        {
            if (Physics.Raycast(weaponMuzzle.position, weaponMuzzle.forward, out RaycastHit hitInfo, weaponItem.maximumDistance, shootableLayers))
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
                        damageable?.TakeDamage(weaponItem.damage * weaponItem.damageMultHead * zombie.zombieDamageMultiplierHead);
                        staggerable?.TryToStagger(1);

                        GameObject bloodSplatter = Instantiate(bloodSplatterFX, hitInfo.transform);
                        bloodSplatter.transform.parent = null;
                    }
                    else if (hitInfo.collider.gameObject.layer == 10)
                    {
                        zombieEffect.DamageZombieTorso();
                        damageable?.TakeDamage(weaponItem.damage * weaponItem.damageMultTorso * zombie.zombieDamageMultiplierTorso);
                        staggerable?.TryToStagger(weaponItem.staggerChance * zombie.zombieStaggerMultiplierTorso);

                        GameObject bloodSplatter = Instantiate(bloodSplatterFX, hitInfo.transform);
                        bloodSplatter.transform.parent = null;
                    }
                    else if (hitInfo.collider.gameObject.layer == 11)
                    {
                        zombieEffect.DamageZombieLeftArm();
                        damageable?.TakeDamage(weaponItem.damage * weaponItem.damageMultArm * zombie.zombieDamageMultiplierArm);
                        staggerable?.TryToStagger(weaponItem.staggerChance * zombie.zombieStaggerMultiplierArm);

                        GameObject bloodSplatter = Instantiate(bloodSplatterFX, hitInfo.transform);
                        bloodSplatter.transform.parent = null;
                    }
                    else if (hitInfo.collider.gameObject.layer == 12)
                    {
                        zombieEffect.DamageZombieRightArm();
                        damageable?.TakeDamage(weaponItem.damage * weaponItem.damageMultArm * zombie.zombieDamageMultiplierArm);
                        staggerable?.TryToStagger(weaponItem.staggerChance * zombie.zombieStaggerMultiplierArm);

                        GameObject bloodSplatter = Instantiate(bloodSplatterFX, hitInfo.transform);
                        bloodSplatter.transform.parent = null;
                    }
                    else if (hitInfo.collider.gameObject.layer == 13)
                    {
                        zombieEffect.DamageZombieLeftLeg();
                        damageable?.TakeDamage(weaponItem.damage * weaponItem.damageMultLeg * zombie.zombieDamageMultiplierLeg);
                        staggerable?.TryToStagger(weaponItem.staggerChance * zombie.zombieStaggerMultiplierLeg);

                        GameObject bloodSplatter = Instantiate(bloodSplatterFX, hitInfo.transform);
                        bloodSplatter.transform.parent = null;
                    }
                    else if (hitInfo.collider.gameObject.layer == 14)
                    {
                        zombieEffect.DamageZombieRightLeg();
                        damageable?.TakeDamage(weaponItem.damage * weaponItem.damageMultLeg * zombie.zombieDamageMultiplierLeg);
                        staggerable?.TryToStagger(weaponItem.staggerChance * zombie.zombieStaggerMultiplierLeg);

                        GameObject bloodSplatter = Instantiate(bloodSplatterFX, hitInfo.transform);
                        bloodSplatter.transform.parent = null;
                    }
                }
                else if (zombieEffect == null)
                {
                    damageable?.TakeDamage(weaponItem.damage);

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

            weaponItem.currentAmmo--;
            playerManager.playerUIManager.ammoMagazineText.text = weaponItem.currentAmmo.ToString();
            timeSinceLastShot = 0;
            OnGunShot();
        }

        else if (CanShoot() && weaponItem.currentAmmo == 0)
        {
            audioSource.PlayOneShot(shotEmptyAudioClip);

            timeSinceLastShot = 0;
            weaponItem.hasShotOnEmpty = true;
        }
    }

    private void OnGunShot()
    {
        audioSource.PlayOneShot(shotAudioClip);

        weaponAnimatorManager.ShootWeapon();
    }
}