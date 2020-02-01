// using System.Collections;
// using RPG.Actions;
// using RPG.Entities;
// using RPG.Items;
// using UnityEngine;
//
// public class LegendaryAbility : Ability
// {
//
//     public bool activated = false;
//
//     private void Start()
//     {
//         InitializeEventSubscribers();
//     }
//
//     public virtual void ActivateCoroutine()
//     {
//         StartCoroutine(CheckIfActivated());
//     }
//
//     public virtual void DeactivateCoroutine()
//     {
//         StopAllCoroutines();
//         if (activated)
//             DeactivateEffect();
//     }
//
//     public virtual bool CheckCondition() => true;
//
//     public virtual void ActivateEffect() { activated = true; }
//
//     public virtual void DeactivateEffect() { activated = false; }
//
//     public virtual void Apply(Entity performer, AttackInfo attackInfo) { }
//
//     public virtual IEnumerator CheckIfActivated()
//     {
//         while (true)
//         {
//             yield return new WaitUntil(() => CheckCondition());
//             ActivateEffect();
//             yield return new WaitWhile(() => CheckCondition());
//             DeactivateEffect();
//         }
//     }
//
//     public virtual void InitializeEventSubscribers()
//     {
//         GetComponent<Item>().ItemEquipped += ActivateCoroutine;
//         GetComponent<Item>().ItemUnequipped += DeactivateCoroutine;
//     }
// }
