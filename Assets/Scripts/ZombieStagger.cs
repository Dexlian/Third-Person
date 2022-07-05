using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStagger : MonoBehaviour, IStaggerable
{
    [SerializeField] ZombieManager zombieManager;

    private void Awake()
    {
        zombieManager = GetComponent<ZombieManager>();
    }

    public void TryToStagger(float staggerChance)
    {
        float random = Random.Range(0f, 1f);

        if (staggerChance >= random)
        {
            //zombieManager.animator.SetTrigger("Stagger");
            zombieManager.animator.SetLayerWeight(1, 0f);
            zombieManager.attackTimer = 0f;
            zombieManager.canAttack = true;
        }
    }
}
