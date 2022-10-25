using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsSound : MonoBehaviour
{
    PlayerMovement playerMovement;
    AudioSource audioSource;

    public AudioClip[] footstepNormal;
    public AudioClip[] footstepMetal;
    public AudioClip[] footstepGrass;


    public float footstepTimer = 0f;
    public float footstepResetTime = 0.2f;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        footstepTimer += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("FloorNormal") && footstepTimer >= footstepResetTime)
        {
            audioSource.volume = (playerMovement.moveSpeed / playerMovement.maximumSpeed) * 0.5f;
            audioSource.PlayOneShot(footstepNormal[Random.Range(0, footstepNormal.Length)]);

            footstepTimer = 0f;
        }
        else if (other.transform.CompareTag("FloorMetal") && footstepTimer >= footstepResetTime)
        {
            audioSource.volume = (playerMovement.moveSpeed / playerMovement.maximumSpeed) * 0.5f;
            audioSource.PlayOneShot(footstepMetal[Random.Range(0, footstepMetal.Length)]);

            footstepTimer = 0f;
        }
        else if (other.transform.CompareTag("FloorGrass") && footstepTimer >= footstepResetTime)
        {
            audioSource.volume = (playerMovement.moveSpeed / playerMovement.maximumSpeed) * 0.5f;
            audioSource.PlayOneShot(footstepGrass[Random.Range(0, footstepMetal.Length)]);

            footstepTimer = 0f;
        }
    }
}
