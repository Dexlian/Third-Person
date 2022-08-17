using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMaster : MonoBehaviour
{
    AnimatorManager characterAnimator;

    public GameObject laserParent;

    public bool laserOn;
    public bool laserAvailable;

    void Start()
    {
        characterAnimator = GetComponent<AnimatorManager>();
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
