using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public PlayerUIManager playerUIManager;
    public PlayerSounds playerSounds;

    public int playerMaxHealth = 100;
    public int playerHealth = 100;

    private void Awake()
    {
        playerInventory = GetComponent<PlayerInventory>();
        playerUIManager = GetComponent<PlayerUIManager>();
        playerSounds = GetComponent<PlayerSounds>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = playerMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamageZombieHit(int damage)
    {
        playerHealth -= damage;

        if (playerHealth > 0)
        {
            playerUIManager.DisplayHealthPopUp();
            playerSounds.PlaySoundPlayerTakesHit();
        }
        else
        {
            //Kill the player
            playerSounds.PlaySoundPlayerDies();
        }
    }
}
