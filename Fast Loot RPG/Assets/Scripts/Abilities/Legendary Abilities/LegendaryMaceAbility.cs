using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegendaryMaceAbility : LegendaryAbility {

    public override void ActivateEffect()
    {
        Player.Instance.statistics.hitChance += 0.5f;
        base.ActivateEffect();
    }

    public override void DeactivateEffect()
    {
        Player.Instance.statistics.dodgeChance -= 0.5f;
        base.DeactivateEffect();
    }

    public override bool CheckCondition()
    {
        if (Player.Instance.statistics.healthPoints <= (int)(Player.Instance.statistics.maxHealthPoints * 0.50))
            return true;
        return false;
    }
}
