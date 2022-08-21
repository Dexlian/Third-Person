using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAimModule : MonoBehaviour
{
    PlayerManager playerManager;

    public LineRenderer laser;
    public Transform laserDot;
    public Transform laserAimModuleTransform;
    public LayerMask shootableLayers;
    public LayerMask laserDotLayers;

    void Start()
    {
        playerManager = GetComponentInParent<PlayerManager>();
    }

    public void AssignLaserAimModule(LaserAimModuleTransform laserAimModuleTransformTarget)
    {
        laserAimModuleTransform = laserAimModuleTransformTarget.transform;
    }

    private void LateUpdate()
    {
        if (laserAimModuleTransform == null)
        {
            return;
        }

        if (playerManager.isAimedIn)
        {
            laser.SetPosition(0, laserAimModuleTransform.position);

            //Laser
            if (Physics.Raycast(laserAimModuleTransform.position, laserAimModuleTransform.forward, out RaycastHit laserRayHit, Mathf.Infinity, shootableLayers))
            {
                laser.SetPosition(1, laserRayHit.point);
            }
            else
            {
                laser.SetPosition(1, laserRayHit.point);
            }

            //Dot
            if (Physics.Raycast(laserAimModuleTransform.position, laserAimModuleTransform.forward, out RaycastHit laserDotHit, Mathf.Infinity, laserDotLayers))
            {
                laserDot.position = laserDotHit.point;
            }
            else
            {
                laserDot.position = Vector3.down * 100f;
            }
        }
    }
}
