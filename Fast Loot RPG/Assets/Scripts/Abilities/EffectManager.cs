using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class EffectManager
{
    public List<Effect> effects = new List<Effect>();

    public EffectManager()
    {
        BattleEventHandler.TurnEnd += () => UpdateDurations(1);
    }

    public void AddEffect(Effect effectToAdd) => effects.Add(effectToAdd);

    public void RemoveEffect(Effect effectToRemove) => effects.Remove(effectToRemove);   

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
