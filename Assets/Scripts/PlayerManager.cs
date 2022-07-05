using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    PlayerSounds playerSounds;

    public int maxHealth = 100;
    public int health = 100;

    private void Awake()
    {
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
        playerSounds.PlaySoundPlayerTakesHit();

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
