using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    //The base class for interactable objects (Items, Doors, Levers, Etc)

    protected PlayerManager player; //the player interacting with the object
    protected Collider interactableCollider; //The collider enabling the interaction when the player is close enough for interaction
    [SerializeField] protected GameObject interactableCanvas; //The image indicating the player can interact with this object

    //ITEM ICON WILL DISAPPEAR WHEN PICKING UP ONE OF TWO ITEMS CLOSE TO EACH OTHER


    protected virtual void OnTriggerEnter(Collider other)
    {
        if (player == null)
        {
            player = other.GetComponent<PlayerManager>();
        }

        if (other.gameObject.CompareTag("Player"))
        {
            if (player != null && !player.isAiming)
            {
                interactableCanvas.SetActive(true);
                player.playerUIManager.itemIcon.SetActive(true);
                //player.canInteract = true;
            }
        }
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (player != null)
            {
                if (!player.isAiming)
                {
                    if (interactableCanvas.activeInHierarchy == false)
                    {
                        interactableCanvas.SetActive(true);
                        player.playerUIManager.itemIcon.SetActive(true);
                    }

                    if (player.inputManager.interactionInput)
                    {
                        Interact(player);
                        player.inputManager.interactionInput = false;
                    }
                }
                else
                {
                    if (interactableCanvas.activeInHierarchy == true)
                    {
                        interactableCanvas.SetActive(false);
                        player.playerUIManager.itemIcon.SetActive(false);
                    }
                }

            }
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        // OPTIONAL: Check for specific layer of collider
        if (other.gameObject.CompareTag("Player"))
        {
            if (player == null)
            {
                player = other.GetComponent<PlayerManager>();
            }

            if (player != null)
            {
                interactableCanvas.SetActive(false);
                player.playerUIManager.itemIcon.SetActive(false);
                //player.canInteract = false;
            }
        }
    }

    //NEED PROPER INTERACTABLE OBJECTS IN FUTURE
    protected virtual void Interact(PlayerManager player)
    {
        if (gameObject.tag == "Door")
        {
            player.playerInteraction.InteractDoor();
        }

        if (gameObject.tag == "ItemPickup")
        {
            player.playerInteraction.InteractItemPickup();
            player.playerUIManager.itemIcon.SetActive(false);
            Destroy(gameObject);
        }
    }
}
