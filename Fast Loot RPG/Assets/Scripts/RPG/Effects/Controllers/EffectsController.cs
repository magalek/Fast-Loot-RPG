using System.Collections.Generic;
using RPG.Entities;
using RPG.Events;

namespace RPG.Effects.Controllers {
    [System.Serializable]
    public class EffectsController
    {
        private Entity _entity;

        public List<Effect> effects = new List<Effect>();

        public EffectsController(Entity entity)
        {
            BattleEvents.TurnEnd += () => UpdateDurations(1);
            _entity = entity;

        }

        public void AddEffect(Effect effectToAdd)
        {
            effects.Add(effectToAdd);
            _entity.statistics += effectToAdd.statistics;
        }

        public void RemoveEffect(Effect effectToRemove)
        {
            effects.Remove(effectToRemove);
            _entity.statistics -= effectToRemove.statistics;
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
