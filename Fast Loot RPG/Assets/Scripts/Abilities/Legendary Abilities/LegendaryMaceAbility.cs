using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegendaryMaceAbility : LegendaryAbility {

    public override void Activate()
    {
        Player.Instance.statistics.hitChance += 0.5f;
        base.Activate();
    }

    public override void Deactivate()
    {
        Player.Instance.statistics.dodgeChance -= 0.5f;
        base.Deactivate();
    }

    public override bool CheckCondition()
    {
        if (Player.Instance.statistics.healthPoints <= (int)(Player.Instance.statistics.maxHealthPoints * 0.50))
            return true;
        return false;
    }
}
