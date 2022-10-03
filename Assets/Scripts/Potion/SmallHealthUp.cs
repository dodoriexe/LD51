using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallHealthUp : Potion
{
    public override void Drink()
    {
        base.Drink();
        level.player.maxHP += 2f;
        level.player.currentHP += 2f;
    }
}
