using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallDamageUp : Potion
{
    public override void Drink()
    {
        base.Drink();
        level.player.combat.AddDamage(2f);
    }
}
