using RPG.Entities;

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

    public override string ToString()
    {
        return
            $"Health Points: {healthPoints}\n" +
            $"Attack: {attack}\n" +
            $"Defense: {defense.ToString("0.00")}\n" +
            $"Hit Chance: {hitChance.ToString("0.00")}\n" +
            $"Dodge Chance: {dodgeChance.ToString("0.00")}\n" +
            $"Block Chance: {blockChance.ToString("0.00")}\n" +
            $"Critical Chance: {criticalChance.ToString("0.00")}\n" +
            $"Critical Damage: {criticalDamage.ToString("0.00")}\n";        
    }
}