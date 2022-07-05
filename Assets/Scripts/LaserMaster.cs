using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMaster : MonoBehaviour
{
    CharacterAnimator characterAnimator;

    public GameObject laserParent;

    public bool laserOn;
    public bool laserAvailable;

    void Start()
    {
        characterAnimator = GetComponent<CharacterAnimator>();
        laserParent.SetActive(false);
    }

    void Update()
    {
        if (characterAnimator.isAimedIn)
        {
            laserParent.SetActive(true);
        }
        else
        {
            laserParent.SetActive(false);
        }
    }
}
