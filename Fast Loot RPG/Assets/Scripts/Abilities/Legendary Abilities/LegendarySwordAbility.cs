using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegendarySwordAbility : LegendaryAbility {

    private void Update()
    {
        if (GetComponent<Item>().equipped)
        {
            if (CheckCondition() && activated == false)
            {
                Activate();
            }
            else if (activated == true)
                Deactivate();
        } 
    }

    public override void Activate()
    {
        Player.Instance.statistics.dodgeChance *= 2;
        activated = true;
    }

    public override void Deactivate()
    {
        Player.Instance.statistics.dodgeChance /= 2;
        activated = false;
    }

    public override bool CheckCondition()
    {
        if (Player.Instance.statistics.healthPoints >= (int)(Player.Instance.statistics.maxHealthPoints * 0.75))
            return true;
        return false;
    }
}
