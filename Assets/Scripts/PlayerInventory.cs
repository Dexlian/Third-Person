using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    PlayerManager player;

    [Header("Keybinds")]
    public KeyCode checkAmmoKey = KeyCode.T;

    [Header("Ammo")]
    public int reservePistolAmmo;
    public int startingReservePistolAmmo;

    private void Awake()
    {
        player = GetComponent<PlayerManager>();
    }
    private void Start()
    {
        reservePistolAmmo = startingReservePistolAmmo;
    }

    private void Update()
    {
        if (Input.GetKeyDown(checkAmmoKey))
        {
            player.playerUIManager.ammoCountFade.CheckMagazineAndReserve();
        }
    }

    public void AddPistolAmmo(int ammo)
    {
        reservePistolAmmo += ammo;
        player.playerUIManager.ammoReserveText.text = player.playerInventory.reservePistolAmmo.ToString();
    }
}
