using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieEffectManager : MonoBehaviour
{
    ZombieManager zombie;

    private void Awake()
    {
        zombie = GetComponent<ZombieManager>();
    }

    public void DamageZombieHead()
    {
        //We ALWAYS stagger on headshot
        //Play proper animation depending on where zombie is shot from
        //Ex, if zombie is shot from behind, the head should move forward
        //if the zombie is shot from the front, the head should move back
        //Play blood/FX at the contact point of the bullet
        zombie.isPerformingAction = true;
        zombie.animator.CrossFade("Damage_Medium_Forward", 0.2f);
    }

    public void DamageZombieTorso()
    {
        //Stagger depending on weapon power
        //Play proper animation depending on where zombie is shot from
        //Play blood/FX at the contact point of the bullet
        zombie.isPerformingAction = true;
        zombie.animator.CrossFade("Damage_Light_Forward", 0.2f);
    }

    public void DamageZombieRightArm()
    {
        //Play proper animation depending on where zombie is shot from
        //Play blood/FX at the contact point of the bullet
        zombie.isPerformingAction = true;
        zombie.animator.CrossFade("Damage_Light_Forward", 0.2f);
    }

    public void DamageZombieLeftArm()
    {
        //Play proper animation depending on where zombie is shot from
        //Play blood/FX at the contact point of the bullet
        zombie.isPerformingAction = true;
        zombie.animator.CrossFade("Damage_Light_Forward", 0.2f);
    }

    public void DamageZombieRightLeg()
    {
        //Play proper animation depending on where zombie is shot from
        //Play blood/FX at the contact point of the bullet
        zombie.isPerformingAction = true;
        zombie.animator.CrossFade("Damage_Light_Forward", 0.2f);
    }

    public void DamageZombieLeftLeg()
    {
        //Play proper animation depending on where zombie is shot from
        //Play blood/FX at the contact point of the bullet
        zombie.isPerformingAction = true;
        zombie.animator.CrossFade("Damage_Light_Forward", 0.2f);
    }
}
