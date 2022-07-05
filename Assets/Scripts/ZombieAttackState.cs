using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackState : ZombieState
{
    ZombieDeathState zombieDeathState;
    ZombieChaseState zombieChaseState;

    ZombieSounds zombieSounds;

    private void Awake()
    {
        zombieDeathState = GetComponent<ZombieDeathState>();
        zombieChaseState = GetComponent<ZombieChaseState>();

        zombieSounds = GetComponentInParent<ZombieSounds>();
    }

    public override ZombieState Tick(ZombieManager zombieManager)
    {
        if (zombieManager.isDead)
        {
            return zombieDeathState;
        }

        //If the zombie is being hurt, or is in some action, pause the state
        if (zombieManager.isPerformingAction)
        {
            zombieManager.animator.SetFloat("Vertical", 0, 0.2f, Time.deltaTime);
            return this;
        }

        zombieManager.animator.SetLayerWeight(1, 1f);
        zombieManager.animator.SetFloat("Vertical", 0, 0.2f, Time.deltaTime);

        if (zombieManager.attackTimer >= zombieManager.attackCooldown && zombieManager.canAttack)
        {
            zombieManager.animator.CrossFade("Attack", 0.2f);
            zombieManager.canAttack = false;

            zombieSounds.AttackSound();
        }

        if (zombieManager.distanceFromCurrentTarget <= zombieManager.minimumAttackDistance)
        {
            return this;
        }
        else
        {
            return zombieChaseState;
        }
    }
}
