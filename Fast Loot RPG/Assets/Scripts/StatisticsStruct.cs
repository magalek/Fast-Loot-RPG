﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    public static Statistics operator +(Statistics a, Statistics b)
    {
        Statistics result = new Statistics();
        result.maxHealthPoints = a.maxHealthPoints + b.healthPoints;
        result.healthPoints = a.healthPoints + b.healthPoints;
        result.attack = a.attack + b.attack;
        result.defense = a.defense + b.defense;
        result.hitChance = a.hitChance + b.hitChance;
        result.dodgeChance = a.dodgeChance + b.dodgeChance;
        result.criticalChance = a.criticalChance + b.criticalChance;
        result.criticalDamage = a.criticalDamage + b.criticalDamage;
        return result;
    }

    public static Statistics operator -(Statistics a, Statistics b)
    {
        Statistics result = new Statistics();
        result.maxHealthPoints = a.maxHealthPoints - b.healthPoints;
        result.healthPoints = a.healthPoints - b.healthPoints;
        result.attack = a.attack - b.attack;
        result.defense = a.defense - b.defense;
        result.hitChance = a.hitChance - b.hitChance;
        result.dodgeChance = a.dodgeChance - b.dodgeChance;
        result.criticalChance = a.criticalChance - b.criticalChance;
        result.criticalDamage = a.criticalDamage - b.criticalDamage;
        return result;
    }

    public void Checkstats(Entity entity)
    {
        if (healthPoints < 0) healthPoints = 0;
        if (attack < 0) attack = 0;
        if (defense < 0) defense = 0;
        if (hitChance < 0) hitChance = 0;
        if (dodgeChance < 0) dodgeChance = 0;
        if (criticalChance < 0) criticalChance = 0;
        if (criticalDamage < 0) criticalDamage = 0;
    }


}