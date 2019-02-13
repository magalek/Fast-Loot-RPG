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

    private void Awake()
    {
        CalculateItemLevel();
        //CheckItemRarity();
    }

    //private void CheckItemRarity()
    //{
    //    switch (rarity)
    //    {
    //        case ItemRarity.Common:
    //            color = Color.green;
    //            break;
    //        case ItemRarity.Legendary:
    //            color = Color.yellow;
    //            break;
    //        default:
    //            break;
    //    }
    //}

    public void CalculateItemLevel()
    {
        itemLevel += statistics.healthPoints;
        itemLevel += statistics.attack;
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
