using RPG.Effects;
using RPG.Entities;
using RPG.UI;

namespace RPG.Actions {
    public class MagicAbility : Ability {

        public string abilityName;

        public Statistics statistics;

        public int cooldown = 0;
        public int maxCooldown;

        public override AttackInfo Invoke(Entity performer, Entity target) {
            //target.statistics += statistics;
            //target.effectsController.AddEffect(new Effect(statistics, maxCooldown, target));

            BattleUI.DisplayMessage($"{performer.entityName} used {abilityName}");
            //cooldown = maxCooldown;
            return new AttackInfo(0, AttackType.Spell, target);
        }

    
    }
}
