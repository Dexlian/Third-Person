using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour, IDamageable
{
    ZombieManager zombieManager;
    ZombieSounds zombieSounds;

    public ZombieRagdoll zombieRagdoll;
    public float health = 80f;
    public float damageSoundTimer = 1f;

    private void Awake()
    {
        zombieManager = GetComponent<ZombieManager>();
        zombieSounds = GetComponent<ZombieSounds>();
    }

    private void Update()
    {
        damageSoundTimer -= 1f * Time.deltaTime;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health > 0 && damageSoundTimer <= 0f)
        {
            zombieSounds.TakesDamageSound();
            damageSoundTimer = 1f;
        }

        if (health <= 0)
        {
            ZombieDies();
        }
    }

    private void ZombieDies()
    {
        zombieRagdoll.DoRagdoll(true);
        zombieManager.isDead = true;
    }
}
