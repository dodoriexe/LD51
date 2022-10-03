using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallLifeReg : Potion
{
    public override void Drink()
    {
        base.Drink();
        level.player.healthPerSecond += .2f;
    }
}
