using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegendaryAbility : Ability {

    public bool activated = false;

    public virtual bool CheckCondition()
    {
        return false;
    }

    public virtual void ActivateEffect()
    {
        activated = true;
    }

    public virtual void DeactivateEffect()
    {
        activated = false;
    }

}
