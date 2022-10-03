using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigHealthUp : Potion
{
    public override void Drink()
    {
        base.Drink();
        level.player.maxHP += 30f;
        level.player.currentHP += 30f;
    }
}
