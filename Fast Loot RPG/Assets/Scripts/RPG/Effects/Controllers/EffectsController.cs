using System.Collections.Generic;
using RPG.Entities;
using RPG.Events;

namespace RPG.Effects.Controllers {
    [System.Serializable]
    public class EffectsController
    {
        private Character m_Character;

        public List<Effect> effects = new List<Effect>();

        public EffectsController(Character character)
        {
            BattleEvents.TurnEnd += () => UpdateDurations(1);
            m_Character = character;

        }

        public void AddEffect(Effect effectToAdd)
        {
            effects.Add(effectToAdd);
            m_Character.stats += effectToAdd.Stats;
        }

        public void RemoveEffect(Effect effectToRemove)
        {
            effects.Remove(effectToRemove);
            m_Character.stats -= effectToRemove.Stats;
        }

        private void UpdateDurations(int amount)
        {
            foreach (Effect effect in effects)
            {
                if (effect != null)
                    effect.Duration -= amount;
            }
        }

        public void DetachEvents() => BattleEvents.TurnEnd -= () => UpdateDurations(1);
    }
}
