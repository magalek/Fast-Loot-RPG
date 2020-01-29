using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class AbilityManager {

    public Ability[] abilities;

    public AbilityManager()
    {
        BattleEventHandler.TurnEnd += () => UpdateCooldowns(amount: 1);
        PlayerEventHandler.PlayerDeath += () => UpdateCooldowns(max: true);
    }

    public Ability GetAbility()
    {
        Ability randomAbility;
        Debug.Log(abilities.Length);
        while (true)
        {
            randomAbility = abilities[Random.Range(0, abilities.Length)];

            if (randomAbility is AttackAbility)
                return randomAbility;
            else if (randomAbility is MagicAbility && ((MagicAbility)randomAbility).cooldown == 0 )
                return randomAbility;
        }
    }

	private void UpdateCooldowns(int amount = 0, bool max = false)
    {
        var abilitiesToRefresh = abilities.Where(a => a is MagicAbility && ((MagicAbility)a).cooldown > 0);
        foreach (var ability in abilitiesToRefresh)
        {
            if (max)
                ((MagicAbility)ability).cooldown = 0;
            else
                ((MagicAbility)ability).cooldown -= amount;
        }
    }

    public void DetachEvents()
    {
        BattleEventHandler.TurnEnd -= () => UpdateCooldowns(amount: 1);
        PlayerEventHandler.PlayerDeath -= () => UpdateCooldowns(max: true);
    }
}
