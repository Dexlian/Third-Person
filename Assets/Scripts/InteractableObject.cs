using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    //The base class for interactable objects (Items, Doors, Levers, Etc)

    protected PlayerManager player; //the player interacting with the object
    protected Collider interactableCollider; //The collider enabling the interaction when the player is close enough for interaction
    [SerializeField] protected GameObject interactableCanvas; //The image indicating the player can interact with this object

    protected virtual void OnTriggerEnter(Collider other)
    {
        // OPTIONAL: Check for specific layer of collider

        if (player == null)
        {
            player = other.GetComponent<PlayerManager>();
        }

        if (player != null)
        {
            interactableCanvas.SetActive(true);
            player.canInteract = true;
        }
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        if (player != null)
        {
            if (player.inputManager.interactionInput)
            {
                Interact(player);
                player.inputManager.interactionInput = false;
            }
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        // OPTIONAL: Check for specific layer of collider

        if (player == null)
        {
            player = other.GetComponent<PlayerManager>();
        }

        if (player != null)
        {
            interactableCanvas.SetActive(false);
            player.canInteract = false;
        }
    }

    protected virtual void Interact(PlayerManager player)
    {
        Debug.Log("You interacted.");
    }
}
