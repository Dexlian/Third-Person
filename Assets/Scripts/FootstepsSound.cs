using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsSound : MonoBehaviour
{
    public PlayerMovement playerMovement;

    public AudioClip[] footstepNormal;
    public AudioClip[] footstepMetal;

    public AudioSource audioSource;

    private float footstepTimer = 0f;

    private void Update()
    {
        footstepTimer += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("FloorNormal") && footstepTimer >= 0.2f)
        {
            audioSource.volume = (playerMovement.moveSpeed / playerMovement.maximumSpeed) * 0.5f;
            audioSource.PlayOneShot(footstepNormal[Random.Range(0, footstepNormal.Length)]);

            footstepTimer = 0f;
        }
        else if (other.transform.CompareTag("FloorMetal") && footstepTimer >= 0.2f)
        {
            audioSource.volume = (playerMovement.moveSpeed / playerMovement.maximumSpeed) * 0.5f;
            audioSource.PlayOneShot(footstepMetal[Random.Range(0, footstepMetal.Length)]);

            footstepTimer = 0f;
        }
    }
}
