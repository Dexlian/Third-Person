using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    AudioSource audioSource;

    public AudioClip[] playerTakesHitSound;
    public AudioClip playerDiesSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundPlayerTakesHit()
    {
        audioSource.PlayOneShot(playerTakesHitSound[Random.Range(0, playerTakesHitSound.Length)]);
    }

    public void PlaySoundPlayerDies()
    {
        audioSource.PlayOneShot(playerDiesSound);
    }
}
