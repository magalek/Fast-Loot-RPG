﻿using UnityEngine;

[System.Serializable]
public struct ItemStatistics
{
    public Vector2 healthPoints;
    public Vector2 attack;
    public Vector2 defense;
    public Vector2 hitChance;
    public Vector2 dodgeChance;
    public Vector2 blockChance;
    public Vector2 criticalChance;
    public Vector2 criticalDamage;

    public Statistics GetRandomStats()
    {
        return new Statistics
        {
            maxHealthPoints = (int)Random.Range(healthPoints.x, healthPoints.y + 1),
            attack = (int)Random.Range(attack.x, attack.y + 1),
            defense = Random.Range(defense.x, defense.y),
            hitChance = Random.Range(hitChance.x, hitChance.y),
            dodgeChance = Random.Range(dodgeChance.x, dodgeChance.y),
            blockChance = Random.Range(blockChance.x, blockChance.y),
            criticalChance = Random.Range(criticalChance.x, criticalChance.y),
            criticalDamage = Random.Range(criticalDamage.x, criticalDamage.y),
        };
    }
}