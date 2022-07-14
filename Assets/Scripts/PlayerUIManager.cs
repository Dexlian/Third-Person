using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    [Header("Ammo")]
    public AmmoCountFade ammoCountFade;
    public Text ammoMagazineText;
    public Text ammoReserveText;

    private void Awake()
    {
        ammoCountFade = FindObjectOfType<AmmoCountFade>();
    }
}
