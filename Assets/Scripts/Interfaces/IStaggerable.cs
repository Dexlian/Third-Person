using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStaggerable
{
    public void TryToStagger(float staggerChance);
}