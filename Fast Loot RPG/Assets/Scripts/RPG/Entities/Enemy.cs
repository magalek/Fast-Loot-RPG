using RPG.Controllers;
using RPG.Entities.Animations;
using RPG.Entities.Movement;
using RPG.Events;
using RPG.Utility;
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

            characterInfo.Health.ChangeCurrentBy(-damage);
            animationController.PlayHit();
            StartCoroutine(enemyController.Pushback());
            if (characterInfo.Health.ZeroOrLess)
                Kill();
        }
        public void CacheComponents() {
            animationController = GetComponent<EnemyAnimationController>();
            enemyController = GetComponent<EnemyController>();
        }
    }
}
