using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    PlayerManager playerManager;

    [Header("Keybinds")]
    public KeyCode checkAmmoKey = KeyCode.T;

    [Header("Ammo")]
    public int reservePistolAmmo;
    public int startingReservePistolAmmo;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
    }
    private void Start()
    {
        reservePistolAmmo = startingReservePistolAmmo;
    }

    private void Update()
    {
        if (Input.GetKeyDown(checkAmmoKey))
        {
            playerManager.playerUIManager.ammoCountFade.CheckMagazineAndReserve();
        }
    }
}
