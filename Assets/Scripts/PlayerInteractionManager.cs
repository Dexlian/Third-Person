using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionManager : MonoBehaviour
{
    PlayerManager player;

    private void Awake()
    {
        player = GetComponent<PlayerManager>();
    }

    //HANDLE PICK UP ITEM INTERACTION (Adds item to inventory)
    public void InteractItemPickup()
    {
        player.playerInventory.AddPistolAmmo(10);
        Debug.Log("Picking up item");
    }

    //HANDLE OPEN DOOR INTERATION (Plays opening door animation)
    public void InteractDoor()
    {
        Debug.Log("Using Door");
    }
}
