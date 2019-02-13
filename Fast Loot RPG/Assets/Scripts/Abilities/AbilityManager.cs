using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class AbilityManager {

    public Ability[] abilities;

    public Ability GetAbility()
    {
        Ability randomAbility;

        while (true)
        {
            randomAbility = abilities[Random.Range(0, abilities.Length)];

            if (randomAbility is AttackAbility)
            {
                return randomAbility;
            }
            else if (randomAbility is MagicAbility && ((MagicAbility)randomAbility).cooldown == 0 )
            {
                return randomAbility;
            }
        }
    }

	public void RefreshCooldowns()
    {
        var abilitiesToRefresh = abilities.Where(a => a is MagicAbility && ((MagicAbility)a).cooldown > 0);
        foreach (var ability in abilitiesToRefresh)
        {
            ((MagicAbility)ability).cooldown--;            
        }
    }
}
