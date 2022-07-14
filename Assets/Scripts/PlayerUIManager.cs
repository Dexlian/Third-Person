using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    PlayerManager playerManager;

    [Header("Health")]
    public StatusPopUpsFade statusPopUpsFade;

    [Header("Ammo")]
    public AmmoCountFade ammoCountFade;
    public Text ammoMagazineText;
    public Text ammoReserveText;

    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        statusPopUpsFade = FindObjectOfType<StatusPopUpsFade>();
        ammoCountFade = FindObjectOfType<AmmoCountFade>();
    }

    public void DisplayHealthPopUp()
    {
        statusPopUpsFade.DisplayHealthPopUp(playerManager.playerHealth);
    }
}
