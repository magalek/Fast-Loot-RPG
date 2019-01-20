using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    public string entityName;
    public int healthPoints = 0;
    public int attack = 0;
    public float defense = 0;
    public float hitChance = 0;
    public float dodgeChance = 0;
    public float blockChance = 0;
    public float criticalChance = 0;
    public float criticalDamage = 0;

    public int CalculateDamage(Entity attacker, Entity defender)
    {
        var chance = attacker.hitChance - (attacker.hitChance * defender.dodgeChance);

        if (chance <= Random.value && defender.blockChance <= Random.value)
        {
            if (attacker.criticalChance >= Random.value)
                return (int)(attacker.attack * attacker.criticalDamage);
            else
                return attacker.attack;
        }
        else
            return 0;
    }

    public void Kill()
    {
        Player.killCount++;
        Destroy(gameObject);
    }
}
