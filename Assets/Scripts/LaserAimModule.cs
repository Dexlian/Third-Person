using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAimModule : MonoBehaviour
{
    AnimatorManager characterAnimator;

    public LineRenderer laser;
    public Transform laserAimModule;
    public Transform laserDot;
    public LayerMask shootableLayers;
    public LayerMask laserDotLayers;

    void Start()
    {
        characterAnimator = GetComponentInParent<AnimatorManager>();
    }

    private void LateUpdate()
    {
        if (characterAnimator.isAimedIn)
        {
            laser.SetPosition(0, laserAimModule.position);

            //Laser
            if (Physics.Raycast(laserAimModule.position, laserAimModule.forward, out RaycastHit laserRayHit, Mathf.Infinity, shootableLayers))
            {
                laser.SetPosition(1, laserRayHit.point);
            }
            else
            {
                laser.SetPosition(1, laserRayHit.point);
            }

            //Dot
            if (Physics.Raycast(laserAimModule.position, laserAimModule.forward, out RaycastHit laserDotHit, Mathf.Infinity, laserDotLayers))
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
