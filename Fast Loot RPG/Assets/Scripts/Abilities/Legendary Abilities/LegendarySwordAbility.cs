using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegendarySwordAbility : LegendaryAbility {

    private void Start()
    {
        GetComponent<Item>().ItemEquipped += ActivateCoroutine;
        GetComponent<Item>().ItemUnequipped += DeactivateCoroutine;
    }

    public void ActivateCoroutine()
    {
        StartCoroutine(CheckIfActivated());
    }

    public void DeactivateCoroutine()
    {
        StopAllCoroutines();
        if (activated)
            DeactivateEffect();
    }

    public override void ActivateEffect()
    {
        Player.Instance.statistics.dodgeChance *= 2;
        activated = true;
    }

    public override void DeactivateEffect()
    {
        Player.Instance.statistics.dodgeChance /= 2;
        activated = false;
    }

    public override bool CheckCondition()
    {
        if (Player.Instance.statistics.healthPoints >= (int)(Player.Instance.statistics.maxHealthPoints * 0.75))
            return true;
        return false;
    }

    IEnumerator CheckIfActivated()
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
