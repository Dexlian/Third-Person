using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public PlayerUIManager playerUIManager;
    public PlayerSounds playerSounds;

    public int maxHealth = 100;
    public int health = 100;

    private void Awake()
    {
        playerInventory = GetComponent<PlayerInventory>();
        playerUIManager = GetComponent<PlayerUIManager>();
        playerSounds = GetComponent<PlayerSounds>();
    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamageZombieHit(int damage)
    {
        health -= damage;

        if (health > 0)
        {
            playerSounds.PlaySoundPlayerTakesHit();
        }
        else
        {
            playerSounds.PlaySoundPlayerDies();
        }
    }
}
