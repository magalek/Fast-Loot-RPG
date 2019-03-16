using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public Sprite sprite;
    public Color color { get
        {
            switch (rarity)
            {
                case ItemRarity.Common:
                    return Color.green;
                case ItemRarity.Legendary:
                    return Color.yellow;
                default:
                    return Color.white;
            }
        }}

    [Space]

    public Statistics statistics;

    public ItemRarity rarity;
    public ItemType type;

    public int itemLevel;

    public bool equipped;

    private void Awake()
    {
        CalculateItemLevel();
    }

    public void CalculateItemLevel()
    {
        itemLevel += statistics.healthPoints;
        itemLevel += statistics.attack;
    }

    public void AddLegendaryAbility()
    {
        switch (type)
        {
            case ItemType.Sword:
                gameObject.AddComponent<LegendarySwordAbility>();
                break;
            case ItemType.Mace:
                break;
            case ItemType.Dagger:
                break;
            case ItemType.Armor:
                break;
            case ItemType.Helmet:
                break;
            case ItemType.Pants:
                break;
            case ItemType.Shield:
                break;
            default:
                break;
        }
    }
}

public enum ItemRarity
{
    Common,
    Legendary
}

public enum ItemType
{
    Sword,
    Mace,
    Dagger,
    Armor,
    Helmet,
    Pants,
    Shield
}
