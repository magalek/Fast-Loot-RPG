using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Statistics", menuName = "Statistics")]
public class Statistics : ScriptableObject {

    public static Statistics operator+ (Statistics a, Statistics b)
    {
        Statistics result = new Statistics();

        result.healthPoints     += b.healthPoints;
        result.attack           += b.attack;
        result.defense          += b.defense;
        result.hitChance        += b.hitChance;
        result.dodgeChance      += b.dodgeChance;
        result.blockChance      += b.blockChance;
        result.criticalChance   += b.criticalChance;
        result.criticalDamage   += b.criticalDamage;

        return result;
    }

    public int healthPoints = 0;
    public int attack = 0;
    public float defense = 0;
    public float hitChance = 0;
    public float dodgeChance = 0;
    public float blockChance = 0;
    public float criticalChance = 0;
    public float criticalDamage = 0;
    
}

