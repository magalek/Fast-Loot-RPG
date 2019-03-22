using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegendarySwordAbility : LegendaryAbility {

    public override void Activate()
    {
        Player.Instance.statistics.dodgeChance *= 2;
        base.Activate();
    }

    public override void Deactivate()
    {
        Player.Instance.statistics.dodgeChance /= 2;
        base.Deactivate();
    }

    public override bool CheckCondition()
    {
        if (Player.Instance.statistics.healthPoints >= (int)(Player.Instance.statistics.maxHealthPoints * 0.75))
            return true;
        return false;
    }

}
