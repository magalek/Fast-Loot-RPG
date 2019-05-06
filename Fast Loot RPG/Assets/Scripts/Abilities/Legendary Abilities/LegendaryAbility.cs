using System.Collections;
using UnityEngine;

public class LegendaryAbility : Ability
{

    public bool activated = false;

    private void Start()
    {
        InitializeEvents();
    }

    public virtual void ActivateCoroutine()
    {
        StartCoroutine(CheckIfActivated());
    }

    public virtual void DeactivateCoroutine()
    {
        StopAllCoroutines();
        if (activated)
            Deactivate();
    }

    public virtual bool CheckCondition() { return true; }

    public virtual void Activate() { activated = true; }

    public virtual void Deactivate() { activated = false; }

    public virtual void Apply(Entity performer, AttackInfo attackInfo) { }

    public virtual IEnumerator CheckIfActivated()
    {
        while (true)
        {
            yield return new WaitUntil(() => CheckCondition());
            Activate();
            yield return new WaitWhile(() => CheckCondition());
            Deactivate();
        }
    }

    public virtual void InitializeEvents()
    {
        GetComponent<Item>().ItemEquipped += ActivateCoroutine;
        GetComponent<Item>().ItemUnequipped += DeactivateCoroutine;
    }
}
