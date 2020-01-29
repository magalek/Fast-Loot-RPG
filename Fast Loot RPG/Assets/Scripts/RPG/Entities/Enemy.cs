using RPG.Items;
using RPG.Managers;
using UnityEngine;

namespace RPG.Entities
{
    public class Enemy : Entity
    {
        public float lootChance;

        public Item DropItem(Enemy enemy = null, float chance = 0)
        {
            if (enemy != null && enemy.lootChance > Random.value)
                return ItemManager.Instance.CreateNewItem();
            else if (enemy == null && chance > Random.value)
                return ItemManager.Instance.CreateNewItem();
            else
                return null;
        }

        public override void Kill()
        {
            EnemyEventHandler.OnEnemyKilled(this);
            base.Kill();
        }
    }
}
