using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public Sprite sprite;
    [ColorUsage(showAlpha:false)] public Color color;

    [Space]

    public int healthPoints;
    public int attack;
    public float defense;
    public float hitChance;
    public float dodgeChance;
    public float blockChance;
    public float criticalChance;
    public float criticalDamage;

    public ItemRarity rarity;
    public ItemType type;

    public int itemLevel;

    private void Awake()
    {
        CalculateItemLevel();
        CheckItemRarity();
    }

    private void CheckItemRarity()
    {
        switch (rarity)
        {
            case ItemRarity.Common:
                color = Color.green;
                break;
            case ItemRarity.Legendary:
                color = Color.yellow;
                break;
            default:
                break;
        }
    }

    public void CalculateItemLevel()
    {
        itemLevel += healthPoints;
        itemLevel += attack;
    }
}

public enum ItemRarity
{
    Common,
    Legendary
}

public enum ItemType
{
    Weapon,
    Armor,
    Shield
}
