using System;
using RPG.Controllers;
using RPG.Entities.Animations;
using RPG.Entities.Movement;
using RPG.Events;
using RPG.Items;
using RPG.Utility;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RPG.Entities
{
    public class Enemy : Character, IHittable, IComponentCache {
        public float lootChance;

        public EnemyAnimationController animationController;
        public EnemyController enemyController;

        public bool IsHittable { get; set; } = true;

        private void Awake() {
            base.Awake();
            CacheComponents();
        }
        
        public override void Kill() {
            EnemyEvents.OnEnemyKilled(this);
            if (lootChance > Random.value) {
                ItemsController.Instance.CreateItemObject(transform.position);
            }
            base.Kill();
        }

        public void Hit(int damage) {
            if (!IsHittable) return;

            health.Subtract(damage);
            animationController.PlayHit();
            StartCoroutine(enemyController.Pushback());
            Debug.Log("hit");
            if (health.zeroOrLess)
                Kill();
        }
        public void CacheComponents() {
            animationController = GetComponent<EnemyAnimationController>();
            enemyController = GetComponent<EnemyController>();
        }
    }
}
