using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegendaryAbility : Ability {

    public bool activated = false;

    private void Start()
    {
        GetComponent<Item>().ItemEquipped += ActivateCoroutine;
        GetComponent<Item>().ItemUnequipped += DeactivateCoroutine;
    }

    public virtual void ActivateCoroutine()
    {
        StartCoroutine(CheckIfActivated());
    }

    public virtual void DeactivateCoroutine()
    {
        StopAllCoroutines();
        if (activated)
            DeactivateEffect();
    }

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

    public virtual IEnumerator CheckIfActivated()
    {
        while (true)
        {
            yield return new WaitUntil(() => CheckCondition());
            ActivateEffect();
            yield return new WaitWhile(() => CheckCondition());
            DeactivateEffect();
        }
    }
}
