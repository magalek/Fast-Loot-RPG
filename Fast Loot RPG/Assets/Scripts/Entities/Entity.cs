using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    public string entityName;

    public Statistics statistics;

    public AbilityManager abilityManager = new AbilityManager();  

    public int CalculateDamage(Entity attacker, Entity defender)
    {
        var chance = attacker.statistics.hitChance - (attacker.statistics.hitChance * defender.statistics.dodgeChance);

        if (chance <= Random.value && defender.statistics.blockChance <= Random.value)
        {
            if (attacker.statistics.criticalChance >= Random.value)
                return (int)(attacker.statistics.attack * attacker.statistics.criticalDamage);
            else
                return attacker.statistics.attack;
        }
        else
            return 0;
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

}
