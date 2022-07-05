using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackState : ZombieState
{
    ZombieDeathState zombieDeathState;
    ZombieChaseState zombieChaseState;

    private void Awake()
    {
        zombieDeathState = GetComponent<ZombieDeathState>();
        zombieChaseState = GetComponent<ZombieChaseState>();
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

        Debug.Log("Attack!");
        zombieManager.animator.SetLayerWeight(1, 1f);
        zombieManager.animator.SetFloat("Vertical", 0, 0.2f, Time.deltaTime);

        if (zombieManager.attackTimer >= zombieManager.attackCooldown && zombieManager.canAttack)
        {
            zombieManager.animator.CrossFade("Attack", 0.2f);
            zombieManager.canAttack = false;
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
