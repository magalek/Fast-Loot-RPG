using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : Entity {

    public float lootChance;

    TextMeshProUGUI hpText;

    private void Awake()
    {
        hpText = GameObject.Find("Enemy HP").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        hpText.text = $"{maxHealthPoints} / {healthPoints}";
    }

    public Item DropItem(Enemy enemy = null, float chance = 0)
    {
        if (enemy != null && enemy.lootChance > Random.value)
        {
            return ItemManager.Instance.GenerateItem();
        }
        else if (enemy == null && chance > Random.value)
        {
            return ItemManager.Instance.GenerateItem();
        }
        else
            return null;
    }
}
