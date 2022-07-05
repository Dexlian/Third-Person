using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieState : MonoBehaviour
{
    //This is the base class for all future states
    public virtual ZombieState Tick(ZombieManager zombieManager)
    {
        return this;
    }
}