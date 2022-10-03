using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigKnockback : Potion
{
    public override void Drink()
    {
        base.Drink();
        level.player.knockback += 35;
    }
}
