using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMaster : MonoBehaviour
{
    PlayerManager playerManager;
    LaserAimModule laserAimModule;

    public GameObject laserParent;

    public bool laserOn;
    public bool laserAvailable;

    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        laserAimModule = GetComponent<LaserAimModule>();
        laserParent.SetActive(false);
    }

    void Update()
    {
        if (playerManager.isAimedIn && laserAimModule.laserAimModuleTransform != null)
        {
            laserParent.SetActive(true);
        }
        else
        {
            laserParent.SetActive(false);
        }
    }
}
