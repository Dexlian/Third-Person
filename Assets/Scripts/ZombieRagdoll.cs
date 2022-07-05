using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieRagdoll : MonoBehaviour
{
    Collider mainCollider;
    Collider[] limbColliders;
    Rigidbody mainRigidbody;
    Rigidbody[] rigidbodies;

    public float rigidbodyLimbMass = 0.1f;

    [Header("Hitboxes")]
    public Collider hitboxTorso;
    public Collider hitboxHead;
    public Collider hitboxUpperLeftArm;
    public Collider hitboxLowerLeftArm;
    public Collider hitboxUpperRightArm;
    public Collider hitboxLowerRightArm;
    public Collider hitboxUpperLeftLeg;
    public Collider hitboxLowerLeftLeg;
    public Collider hitboxUpperRightLeg;
    public Collider hitboxLowerRightLeg;

    // Start is called before the first frame update
    void Awake()
    {
        mainCollider = GetComponent<Collider>();
        limbColliders = GetComponentsInChildren<Collider>(true);
        mainRigidbody = GetComponent<Rigidbody>();
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        DoRagdoll(false);
    }

    public void DoRagdoll(bool isRagdoll)
    {
        foreach (Collider collider in limbColliders)
        {
            collider.enabled = isRagdoll;
        }

        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = !isRagdoll;
            rigidbody.mass = rigidbodyLimbMass;
        }

        mainCollider.enabled = !isRagdoll;
        mainRigidbody.mass = 1f;
        mainRigidbody.useGravity = !isRagdoll;
        mainRigidbody.isKinematic = isRagdoll;
        GetComponent<Animator>().enabled = !isRagdoll;

        hitboxTorso.enabled = true;
        hitboxHead.enabled = true;
        hitboxUpperLeftArm.enabled = true;
        hitboxLowerLeftArm.enabled = true;
        hitboxUpperRightArm.enabled = true;
        hitboxLowerRightArm.enabled = true;
        hitboxUpperLeftLeg.enabled = true;
        hitboxLowerLeftLeg.enabled = true;
        hitboxUpperRightLeg.enabled = true;
        hitboxLowerRightLeg.enabled = true;
    }
}
