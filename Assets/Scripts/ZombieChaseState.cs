using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieChaseState : ZombieState
{
    ZombieDeathState zombieDeathState;
    ZombieAttackState zombieAttackState;

    private void Awake()
    {
        zombieDeathState = GetComponent<ZombieDeathState>();
        zombieAttackState = GetComponent<ZombieAttackState>();
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

        MoveTowardsCurrentTarget(zombieManager);
        RotateTowardsCurrentTarget(zombieManager);

        if (zombieManager.distanceFromCurrentTarget <= zombieManager.minimumAttackDistance)
        {
            return zombieAttackState;
        }
        else
        {
            return this;
        }
    }

    private void MoveTowardsCurrentTarget(ZombieManager zombieManager)
    {
        if (zombieManager.isDead)
            return;

        zombieManager.animator.SetFloat("Vertical", zombieManager.zombieSpeed, 0.2f, Time.deltaTime);
    }

    private void RotateTowardsCurrentTarget(ZombieManager zombieManager)
    {
        if (zombieManager.isDead)
            return;

        zombieManager.zombieNavMeshAgent.enabled = true;

        zombieManager.zombieNavMeshAgent.SetDestination(zombieManager.currentTarget.transform.position);

        zombieManager.transform.rotation = Quaternion.Slerp(zombieManager.transform.rotation,
            zombieManager.zombieNavMeshAgent.transform.rotation,
            zombieManager.rotationSpeed / Time.deltaTime);
    }
}
