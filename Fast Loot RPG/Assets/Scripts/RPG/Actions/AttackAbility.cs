﻿using RPG.Entities;
using RPG.UI;
using UnityEngine;

namespace RPG.Actions {
    public class AttackAbility : Ability {

        public override AttackInfo Invoke(Entity performer, Entity target)
        {
            int attackerDamage = CalculateDamage(performer, target, out AttackInfo attackInfo);

            target.statistics.healthPoints -= attackerDamage;

            BattleUI.DisplayMessage($"{performer.entityName} hit {target.entityName} for {attackerDamage}");

            return attackInfo;
        }

        private int CalculateDamage(Entity attacker, Entity target, out AttackInfo attackInfo)
        {
            var chance = attacker.statistics.hitChance - (attacker.statistics.hitChance * target.statistics.dodgeChance);

            if (chance >= Random.value && target.statistics.blockChance <= Random.value)
            {
                if (attacker.statistics.criticalChance >= Random.value)
                {
                    int damage = (int)(attacker.statistics.attack * attacker.statistics.criticalDamage);

                    attackInfo = new AttackInfo(damage, AttackType.Critical, target);

                    return damage;
                }
                else
                {
                    int damage = attacker.statistics.attack;

                    attackInfo = new AttackInfo(damage, AttackType.Normal, target);

                    return damage;
                }
            }
            else
            {
                attackInfo = new AttackInfo(0, AttackType.Normal, target);

                return 0;
            }
        }
    }
}
