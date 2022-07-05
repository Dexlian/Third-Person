using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    [SerializeField] ZombieManager zombieManager;

    private void Awake()
    {
        zombieManager = GetComponent<ZombieManager>();
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
                Debug.Log("Zombie misses the player.");
            }
            else
            {

                Debug.Log("Zombie hits the player!");
            }
        }
    }
}
