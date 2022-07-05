using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSoundsFootsteps : MonoBehaviour
{
    ZombieManager zombieManager;
    AudioSource audioSource;

    public AudioClip[] footstepNormal;
    public AudioClip[] footstepMetal;

    private float footstepTimer = 0f;

    private void Awake()
    {
        zombieManager = GetComponent<ZombieManager>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        footstepTimer += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("FloorNormal") && footstepTimer >= 0.2f)
        {
            Debug.Log("Step");

            audioSource.volume = (zombieManager.zombieSpeed / zombieManager.zombieMaxSpeed) * 1.5f;
            audioSource.PlayOneShot(footstepNormal[Random.Range(0, footstepNormal.Length)]);

            footstepTimer = 0f;
        }
        else if (other.transform.CompareTag("FloorMetal") && footstepTimer >= 0.2f)
        {
            audioSource.volume = (zombieManager.zombieSpeed / zombieManager.zombieMaxSpeed) * 1.5f;
            audioSource.PlayOneShot(footstepMetal[Random.Range(0, footstepMetal.Length)]);

            footstepTimer = 0f;
        }
    }
}
