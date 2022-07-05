using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    ZombieManager zombieManager;
    ZombieSounds zombieSounds;

    private void Awake()
    {
        zombieManager = GetComponent<ZombieManager>();
        zombieSounds = GetComponent<ZombieSounds>();
    }

    private void AttackEvent()
    {
        zombieManager.attackTimer = 0f;
        zombieManager.animator.SetLayerWeight(1, 0f);
        zombieManager.canAttack = true;

        if (zombieManager.isPerformingAction == false)
        {
            if (zombieManager.distanceFromCurrentTarget > zombieManager.minimumAttackDistance + zombieManager.attackRangeBuffer)
            {
                zombieSounds.MissSound();
            }
            else
            {
                zombieManager.currentTarget.TakeDamageZombieHit(zombieManager.damageHit);
                Debug.Log(zombieManager.currentTarget.health);
            }
        }
    }
}
