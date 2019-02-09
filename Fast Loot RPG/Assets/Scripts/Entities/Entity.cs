using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    public string entityName;

    #region Stats
    public int maxHealthPoints = 0;
    public int healthPoints = 0;
    public int attack = 0;
    public float defense = 0;
    public float hitChance = 0;
    public float dodgeChance = 0;
    public float blockChance = 0;
    public float criticalChance = 0;
    public float criticalDamage = 0;
    #endregion

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
        Destroy(gameObject);
    }

    public void AddStatistics(
        Entity entity,
        Item item = null,
        int healthPoints = 0, 
        int attack = 0, 
        float defense = 0, 
        float hitChance = 0, 
        float dodgeChance = 0,
        float criticalChance = 0,
        float criticalDamage = 0
        )
    {
        if (item == null)
        {
            entity.maxHealthPoints  += healthPoints;
            entity.healthPoints     += healthPoints;
            entity.attack           += attack;
            entity.defense          += defense;
            entity.hitChance        += hitChance;
            entity.dodgeChance      += dodgeChance;
            entity.criticalChance   += criticalChance;
            entity.criticalDamage   += criticalDamage; 
        }
        else
        {
            entity.maxHealthPoints  += item.healthPoints;
            entity.healthPoints     += item.healthPoints;
            entity.attack           += item.attack;
            entity.defense          += item.defense;
            entity.hitChance        += item.hitChance;
            entity.dodgeChance      += item.dodgeChance;
            entity.criticalChance   += item.criticalChance;
            entity.criticalDamage   += item.criticalDamage;
        }       
    }

    public void RemoveStatistics(
        Entity entity,
        Item item = null,
        int healthPoints = 0,
        int attack = 0,
        float defense = 0,
        float hitChance = 0,
        float dodgeChance = 0,
        float criticalChance = 0,
        float criticalDamage = 0
    )
    {
        if (item == null)
        {
            entity.maxHealthPoints  -= healthPoints;
            entity.healthPoints     -= healthPoints;
            entity.attack           -= attack;
            entity.defense          -= defense;
            entity.hitChance        -= hitChance;
            entity.dodgeChance      -= dodgeChance;
            entity.criticalChance   -= criticalChance;
            entity.criticalDamage   -= criticalDamage;
        }
        else
        {
            entity.maxHealthPoints  -= item.healthPoints;
            entity.healthPoints     -= item.healthPoints;
            entity.attack           -= item.attack;
            entity.defense          -= item.defense;
            entity.hitChance        -= item.hitChance;
            entity.dodgeChance      -= item.dodgeChance;
            entity.criticalChance   -= item.criticalChance;
            entity.criticalDamage   -= item.criticalDamage;
        }
    }
}
