using System.Collections.Generic;

[System.Serializable]
public class Battle
{

    public static Battle Actual;

    public List<Turn> turns = new List<Turn>();

    public Enemy enemy;

    public Battle()
    {
        Actual = this;
        GameManager.Instance.battles.Add(this);
        Turn.allTurnsNumbers = 0;
    }
}
