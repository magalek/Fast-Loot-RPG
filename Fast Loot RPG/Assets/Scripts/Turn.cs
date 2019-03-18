using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Turn {

    public static int allTurnsNumbers;

    public Entity attacker;
    public Entity target;
    public AttackInfo attackInfo;
    public int turnNumber = 0;

    public Turn(Entity attacker, Entity target, AttackInfo attackInfo)
    {
        this.attacker = attacker;
        this.target = target;
        this.attackInfo = attackInfo;
        turnNumber = allTurnsNumbers;
        allTurnsNumbers++;
    }
}
