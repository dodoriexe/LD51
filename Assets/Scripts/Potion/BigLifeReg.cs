using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigLifeReg : Potion
{
    public override void Drink()
    {
        base.Drink();
        level.player.healthPerSecond += 2f;
    }
}
