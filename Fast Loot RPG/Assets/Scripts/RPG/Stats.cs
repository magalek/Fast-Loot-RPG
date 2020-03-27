using RPG.Entities;
using UnityEngine.UIElements;

[System.Serializable]
public class Stats 
{
    public int attack;
    public float defense;
    public float hitChance;
    public float dodgeChance;
    public float blockChance;
    public float criticalChance;
    public float criticalDamage;

    public static Stats operator +(Stats a, Stats b)
    {
        Stats result = new Stats {
            //maxHealthPoints = a.maxHealthPoints + b.healthPoints,
            //healthPoints = a.healthPoints + b.healthPoints,
            attack = a.attack + b.attack,
            defense = a.defense + b.defense,
            hitChance = a.hitChance + b.hitChance,
            dodgeChance = a.dodgeChance + b.dodgeChance,
            criticalChance = a.criticalChance + b.criticalChance,
            criticalDamage = a.criticalDamage + b.criticalDamage
        };
        return result;
    }

    public static Stats operator -(Stats a, Stats b)
    {
        Stats result = new Stats {
            //maxHealthPoints = a.maxHealthPoints - b.healthPoints,
            //healthPoints = a.healthPoints - b.healthPoints,
            attack = a.attack - b.attack,
            defense = a.defense - b.defense,
            hitChance = a.hitChance - b.hitChance,
            dodgeChance = a.dodgeChance - b.dodgeChance,
            criticalChance = a.criticalChance - b.criticalChance,
            criticalDamage = a.criticalDamage - b.criticalDamage
        };
        return result;
    }

    public void Checkstats(Entity entity)
    {
        //if (healthPoints < 0) healthPoints = 0;
        if (attack < 0) attack = 0;
        if (defense < 0) defense = 0;
        if (hitChance < 0) hitChance = 0;
        if (dodgeChance < 0) dodgeChance = 0;
        if (criticalChance < 0) criticalChance = 0;
        if (criticalDamage < 0) criticalDamage = 0;
    }

    public override string ToString()
    {
        return
            //$"Health Points: {healthPoints}\n" +
            $"Attack: {attack}\n" +
            $"Defense: {defense.ToString("0.00")}\n" +
            $"Hit Chance: {hitChance.ToString("0.00")}\n" +
            $"Dodge Chance: {dodgeChance.ToString("0.00")}\n" +
            $"Block Chance: {blockChance.ToString("0.00")}\n" +
            $"Critical Chance: {criticalChance.ToString("0.00")}\n" +
            $"Critical Damage: {criticalDamage.ToString("0.00")}\n";        
    }
}