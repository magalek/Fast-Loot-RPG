using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class EffectManager
{
    private Entity _entity;

    public List<Effect> effects = new List<Effect>();

    public EffectManager(Entity entity)
    {
        BattleEventHandler.TurnEnd += () => UpdateDurations(1);
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

    public void DetachEvents() => BattleEventHandler.TurnEnd -= () => UpdateDurations(1);
}
