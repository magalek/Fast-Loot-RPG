using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public delegate void ItemDelegate();
    public event ItemDelegate ItemEquipped;
    public event ItemDelegate ItemUnequipped;

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

    [Header("Stats")]

    public ItemStatistics statRanges;
    public Statistics statistics;

    public ItemRarity rarity;
    public ItemType type;

    public int itemLevel;

    public bool equipped;

    private void Awake()
    {
        CalculateItemLevel();
    }

    public void OnItemEquipped()
    {
        ItemEquipped?.Invoke();
    }

    public void OnItemUnequipped()
    {
        ItemUnequipped?.Invoke();
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
                gameObject.AddComponent<LegendaryMaceAbility>();
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
