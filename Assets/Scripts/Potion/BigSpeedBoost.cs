using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSpeedBoost : Potion
{
    public override void Drink()
    {
        base.Drink();
        level.player.moveSpeed += 2f;
    }
}
