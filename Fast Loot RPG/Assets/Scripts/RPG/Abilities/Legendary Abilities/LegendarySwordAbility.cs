using RPG.Entities;

public class LegendarySwordAbility : LegendaryAbility
{
    float statDifference = 0;

    public override void ActivateEffect()
    {
        statDifference = Player.Instance.statistics.dodgeChance * 2;
        Player.Instance.statistics.dodgeChance += statDifference;
        base.ActivateEffect();
    }

    public override void DeactivateEffect()
    {
        Player.Instance.statistics.dodgeChance -= statDifference;
        base.DeactivateEffect();
    }

    public override bool CheckCondition()
    {
        if (Player.Instance.statistics.healthPoints >= 
            (int)(Player.Instance.statistics.maxHealthPoints * 0.75))
            return true;
        return false;
    }

}
