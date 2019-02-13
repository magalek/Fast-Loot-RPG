using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Statistics
{
    public int maxHealthPoints;
    public int healthPoints;
    public int attack;
    public float defense;
    public float hitChance;
    public float dodgeChance;
    public float blockChance;
    public float criticalChance;
    public float criticalDamage;

    public static Statistics operator+ (Statistics a, Statistics b)
    {
        Statistics result = new Statistics();
        result.maxHealthPoints  =    a.maxHealthPoints +     b.healthPoints;
        result.healthPoints     =     a.healthPoints +        b.healthPoints;
        result.attack           =           a.attack +              b.attack;
        result.defense          =          a.defense +             b.defense;
        result.hitChance        =        a.hitChance +           b.hitChance;
        result.dodgeChance      =      a.dodgeChance +         b.dodgeChance;
        result.criticalChance   =  a.criticalChance +      b.criticalChance;
        result.criticalDamage   = a.criticalDamage +      b.criticalDamage;
        return result;
    }

    public static Statistics operator -(Statistics a, Statistics b)
    {
        Statistics result = new Statistics();
        result.maxHealthPoints  = a.maxHealthPoints - b.healthPoints;
        result.healthPoints     = a.healthPoints - b.healthPoints;
        result.attack           = a.attack - b.attack;
        result.defense          = a.defense - b.defense;
        result.hitChance        = a.hitChance - b.hitChance;
        result.dodgeChance      = a.dodgeChance - b.dodgeChance;
        result.criticalChance   = a.criticalChance - b.criticalChance;
        result.criticalDamage   = a.criticalDamage - b.criticalDamage;
        return result;
    }

}
    

