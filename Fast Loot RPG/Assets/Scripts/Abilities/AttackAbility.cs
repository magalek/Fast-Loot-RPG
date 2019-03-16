using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAbility : Ability {

    public override AttackInfo Invoke(Entity performer, Entity target)
    {
        AttackInfo attackInfo;
        int attackerDamage = CalculateDamage(performer, target, out attackInfo);

        target.statistics.healthPoints -= attackerDamage;

        BattleLog.Instance.SendMessageToBattleLog($"{performer.entityName} hit {target.entityName} for {attackerDamage}");

        return attackInfo;
    }

    public int CalculateDamage(Entity attacker, Entity target, out AttackInfo attackInfo)
    {
        var chance = attacker.statistics.hitChance - (attacker.statistics.hitChance * target.statistics.dodgeChance);

        if (chance >= Random.value && target.statistics.blockChance <= Random.value)
        {
            if (attacker.statistics.criticalChance >= Random.value)
            {
                attackInfo = AttackInfo.Critical;
                return (int)(attacker.statistics.attack * attacker.statistics.criticalDamage);
            }
            else
            {
                attackInfo = AttackInfo.Normal;
                return attacker.statistics.attack;
            }
        }
        else
        {
            attackInfo = AttackInfo.None;
            return 0;
        }
    }
}
