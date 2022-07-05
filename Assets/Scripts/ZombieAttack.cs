using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    ZombieManager zombieManager;
    AudioSource audioSource;

    public AudioClip[] zombieAttackShortSound;



    private void Awake()
    {
        zombieManager = GetComponent<ZombieManager>();
        audioSource = GetComponent<AudioSource>();
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
                zombieManager.currentTarget.TakeDamageZombieHit(zombieManager.damageHit);
                Debug.Log(zombieManager.currentTarget.health);
            }
        }
    }

    public void AttackSound()
    {
        audioSource.PlayOneShot(zombieAttackShortSound[Random.Range(0, zombieAttackShortSound.Length)]);
    }
}
