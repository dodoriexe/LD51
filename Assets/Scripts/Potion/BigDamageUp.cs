using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigDamageUp : Potion
{
    public override void Drink()
    {
        base.Drink();
        level.player.combat.AddDamage(8f);
    }
}
