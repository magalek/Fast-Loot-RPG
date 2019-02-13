using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAbility : Ability {

    public override void Execute(Entity performer, Entity target)
    {
        int attackerDamage = performer.CalculateDamage(performer, target);
        target.statistics.healthPoints -= attackerDamage;

        BattleLog.Instance.SendMessageToBattleLog($"{performer.entityName} hit {target.entityName} for {attackerDamage}");
    }
}
