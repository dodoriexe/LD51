using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallSpeedBoost : Potion
{
    public override void Drink()
    {
        base.Drink();
        level.player.moveSpeed += .2f;
    }
}
