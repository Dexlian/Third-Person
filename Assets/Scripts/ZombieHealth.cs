using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour, IDamageable
{
    ZombieManager zombieManager;

    public ZombieRagdoll zombieRagdoll;
    public float health = 30f;

    private void Awake()
    {
        zombieManager = GetComponent<ZombieManager>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

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
