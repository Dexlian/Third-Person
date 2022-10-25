using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionManager : MonoBehaviour
{
    AudioManager audioManager;
    PlayerManager player;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        player = GetComponent<PlayerManager>();
    }

    //HANDLE PICK UP ITEM INTERACTION (Adds item to inventory)
    public void InteractItemPickup()
    {
        audioManager.Play("sound_ammo_pickup");
        player.playerInventory.AddPistolAmmo(10);
        Debug.Log("Picking up item");
    }

    //HANDLE OPEN DOOR INTERATION (Plays opening door animation)
    public void InteractDoor()
    {
        Debug.Log("Using Door");
    }
}
