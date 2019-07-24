using UnityEngine;

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
        //hpText.text = "Dead";
        base.Kill();
    }
}
