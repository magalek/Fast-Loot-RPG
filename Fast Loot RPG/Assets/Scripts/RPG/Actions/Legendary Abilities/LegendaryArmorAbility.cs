// using System.Collections;
// using System.Collections.Generic;
// using RPG.Entities;
// using RPG.Events;
// using UnityEngine;
//
// public class LegendaryArmorAbility : LegendaryAbility {
//
//     float chance = 0.2f;
//
//     public override void ActivateEffect()
//     {
//         BattleEvents.ActionDone += Apply;
//
//         base.ActivateEffect();
//     }
//
//     public override void DeactivateEffect()
//     {
//         BattleEvents.ActionDone -= Apply;
//
//         base.DeactivateEffect();
//     }
//
//     public override IEnumerator CheckIfActivated()
//     {
//         ActivateEffect();
//
//         yield return null;
//     }
//
//     public override void Apply(Entity performer, AttackInfo attackInfo)
//     {
//         if (performer is Enemy) 
//             if (attackInfo.type == AttackType.Normal || attackInfo.type == AttackType.Critical)
//                 if (Random.value <= chance)
//                 {
//                     performer.statistics.healthPoints -= (int)(attackInfo.damage * 0.50f);
//                     EnemyEvents.OnEnemyHit(performer as Enemy);                   
//                 }
//             
//     }
//
//
// }
