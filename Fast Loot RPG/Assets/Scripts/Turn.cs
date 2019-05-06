[System.Serializable]
public class Turn
{

    public static int allTurnsNumbers;

    public Entity attacker;
    public Entity target;
    public AttackType attackInfo;
    public int turnNumber = 0;

    public Turn(Entity attacker, Entity target, AttackType attackInfo)
    {
        this.attacker = attacker;
        this.target = target;
        this.attackInfo = attackInfo;
        turnNumber = allTurnsNumbers;
        allTurnsNumbers++;
    }
}
