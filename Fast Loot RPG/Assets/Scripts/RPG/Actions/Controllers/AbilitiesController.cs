using System.Linq;
using RPG.Events;
using UnityEngine;

namespace RPG.Actions.Controllers {
    [System.Serializable]
    public class AbilitiesController {

        public Ability[] abilities;

        public AbilitiesController()
        {
            BattleEvents.TurnEnd += () => UpdateCooldowns(1);
            PlayerEvents.PlayerDeath += () => UpdateCooldowns(max: true);
        }

        public Ability GetAbility()
        {
            while (true) {
                var randomAbility = abilities[Random.Range(0, abilities.Length)];

                switch (randomAbility) {
                    case AttackAbility _:
                        return randomAbility;
                    case MagicAbility ability when ability.cooldown == 0:
                        return ability;
                }
            }
        }

        private void UpdateCooldowns(int amount = 0, bool max = false)
        {
            var abilitiesToRefresh = abilities.Where(a => a is MagicAbility && ((MagicAbility)a).cooldown > 0);
            foreach (var ability in abilitiesToRefresh)
            {
                if (max)
                    ((MagicAbility)ability).cooldown = 0;
                else
                    ((MagicAbility)ability).cooldown -= amount;
            }
        }

        public void DetachEvents()
        {
            BattleEvents.TurnEnd -= () => UpdateCooldowns(amount: 1);
            PlayerEvents.PlayerDeath -= () => UpdateCooldowns(max: true);
        }
    }
}
