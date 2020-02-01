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

        public Item DropItem(Enemy enemy = null, float chance = 0) {
            if (enemy != null && enemy.lootChance > Random.value)
                return ItemsController.Instance.CreateNewItem();
            if (enemy == null && chance > Random.value)
                return ItemsController.Instance.CreateNewItem();
            return null;
        }

        protected override void Kill()
        {
            EnemyEvents.OnEnemyKilled(this);
            base.Kill();
        }

        protected override void SetTag() {
            tag = "Enemy";
        }
    }
}
