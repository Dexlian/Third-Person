using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDeathState : ZombieState
{
    public override ZombieState Tick(ZombieManager zombieManager)
    {
        return this;
    }
}
