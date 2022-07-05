using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieIdleState : ZombieState
{
    ZombieDeathState zombieDeathState;
    ZombieChaseState zombieChaseState;

    [Header("Detection Layer")]
    [SerializeField] LayerMask detectionLayer;

    //This setting determines where our linecast starts on the Y-Axis of the characters (used for line of sight)
    [Header("Line of Sight Detection")]
    [SerializeField] float characterHeight = 1.6f;
    [SerializeField] Transform zombieEyes;
    [SerializeField] LayerMask ignoreForLineOfSightDetection;

    //Detects targets within RANGE
    [Header("Detection Range")]
    [SerializeField] float detectionRangeSight = 20f;

    //Detects targets within angle (FIELD OF VIEW)
    [Header("Detection Angle")]
    [SerializeField] float minimumDetectionAngleSight = -50f;
    [SerializeField] float maximumDetectionAngleSight = 50f;

    private void Awake()
    {
        zombieDeathState = GetComponent<ZombieDeathState>();
        zombieChaseState = GetComponent<ZombieChaseState>();
    }


    public override ZombieState Tick(ZombieManager zombieManager)
    {
        if (zombieManager.isDead)
        {
            return zombieDeathState;
        }

        //LOGIC TO FIND A TARGET GOES HERE

        if (zombieManager.currentTarget != null)
        {
            return zombieChaseState;
        }
        else
        {
            FindATargetViaLineOfSight(zombieManager);
            return this;
        }
    }

    private void FindATargetViaLineOfSight(ZombieManager zombieManager)
    {
        //We are searching ALL colliders on the layer of the PLAYER within a certain radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRangeSight, detectionLayer);

        //Debug.Log("We are checking for colliders");

        //For every collider that we find, that is on the same layer of the player, we tray and search it for a PlayerManager script
        for (int i = 0; i < colliders.Length; i++)
        {
            PlayerManager player = colliders[i].transform.GetComponent<PlayerManager>();

            //If the PlayerManager is detected, we then check for line of sight
            if (player != null)
            {
                //Debug.Log("We found the player collider");

                //The target must be in front of us
                Vector3 targetDirection = transform.position - player.transform.position;
                float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                if (viewableAngle > minimumDetectionAngleSight && viewableAngle < maximumDetectionAngleSight)
                {
                    //Debug.Log("We passed the field of view check");

                    Vector3 playerStartPoint = new Vector3(player.transform.position.x, characterHeight, player.transform.position.z);
                    Vector3 zombieStartPoint = zombieEyes.transform.position;

                    Debug.DrawLine(playerStartPoint, zombieStartPoint, Color.yellow);

                    //CHECK ONE LAST TIME FOR OBJECT BLOCKING VIEW
                    if (Physics.Linecast(playerStartPoint, zombieStartPoint))
                    {
                        //Debug.Log("There is something in the way");
                        //Cannot find the target, there is an object in the way
                    }
                    else
                    {
                        //Debug.Log("We have a target - switching states");
                        zombieManager.currentTarget = player;
                    }
                }
            }
        }
    }
}
