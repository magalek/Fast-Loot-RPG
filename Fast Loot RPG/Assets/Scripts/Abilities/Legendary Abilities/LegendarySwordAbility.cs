using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegendarySwordAbility : LegendaryAbility {

    public override void ActivateEffect()
    {
        Player.Instance.statistics.dodgeChance *= 2;
        base.ActivateEffect();
    }

    public override void DeactivateEffect()
    {
        Player.Instance.statistics.dodgeChance /= 2;
        base.DeactivateEffect();
    }

    public override bool CheckCondition()
    {
        if (Player.Instance.statistics.healthPoints >= (int)(Player.Instance.statistics.maxHealthPoints * 0.75))
            return true;
        return false;
    }

}
