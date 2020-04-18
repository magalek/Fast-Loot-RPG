using System;
using RPG.Controllers;
using RPG.Entities.Animations;
using RPG.Events;
using RPG.Items;
using RPG.Utility;
using Random = UnityEngine.Random;

namespace RPG.Entities
{
    public class Enemy : Character, IHittable, IComponentCache {
        public float lootChance;

        public EnemyAnimationController animationController;
        
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
            health.Subtract(damage);
            animationController.PlayHit();
            
            if (health.zeroOrLess)
                Kill();
        }

        public void CacheComponents() {
            animationController = GetComponent<EnemyAnimationController>();
        }
    }
}
