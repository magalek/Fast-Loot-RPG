using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegendaryArmorAbility : LegendaryAbility {

    float chance = 0.2f;

    public override void Activate()
    {
        BattleEventHandler.ActionDone += Apply;

        base.Activate();
    }

    public override void Deactivate()
    {
        BattleEventHandler.ActionDone -= Apply;

        base.Deactivate();
    }

    public override IEnumerator CheckIfActivated()
    {
        Activate();

        yield return null;
    }

    public override void Apply(Entity performer, AttackInfo attackInfo)
    {
        if (performer is Enemy) 
            if (attackInfo.type == AttackType.Normal || attackInfo.type == AttackType.Critical)
                if (Random.value <= chance)
                {
                    performer.statistics.healthPoints -= (int)(attackInfo.damage * 0.50f);
                    EnemyEventHandler.OnEnemyHit(performer as Enemy);                   
                }
            
    }


}
