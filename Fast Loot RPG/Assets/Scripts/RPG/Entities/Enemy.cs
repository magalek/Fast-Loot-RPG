using System;
using RPG.Controllers;
using RPG.Events;
using RPG.Items;
using Random = UnityEngine.Random;

namespace RPG.Entities
{
    public class Enemy : Entity
    {
        public float lootChance;

        private void Awake() {
            SetTag();
            SetComponents();
        }
        
        protected override void Kill() {
            EnemyEvents.OnEnemyKilled(this);
            if (lootChance > Random.value) {
                ItemsController.DropItemAtPosition(transform.position);
            }
            base.Kill();
        }

        protected override void SetTag() {
            tag = "Enemy";
        }
    }
}
