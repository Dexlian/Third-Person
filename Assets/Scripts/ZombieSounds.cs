using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSounds : MonoBehaviour
{
    AudioSource audioSource;

    public AudioClip[] zombieAttackShortSound;
    public AudioClip[] zombieMissSound;
    public AudioClip[] zombieTakesDamageSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void AttackSound()
    {
        audioSource.PlayOneShot(zombieAttackShortSound[Random.Range(0, zombieAttackShortSound.Length)]);
    }

    public void MissSound()
    {
        audioSource.PlayOneShot(zombieMissSound[Random.Range(0, zombieMissSound.Length)]);
    }

    public void TakesDamageSound()
    {
        audioSource.volume = 0.5f;
        audioSource.PlayOneShot(zombieTakesDamageSound[Random.Range(0, zombieTakesDamageSound.Length)]);
    }
}
