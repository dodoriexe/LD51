using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallKnockbackBoost : Potion
{
    public override void Drink()
    {
        base.Drink();
        level.player.knockback += 5;
    }
}
