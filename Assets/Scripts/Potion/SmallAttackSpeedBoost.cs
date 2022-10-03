using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallAttackSpeedBoost : Potion
{

    public override void Drink()
    {
        base.Drink();
        level.player.attackSpeed -= .2f;
    }
}
