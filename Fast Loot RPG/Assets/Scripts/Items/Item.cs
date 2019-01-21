﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

    public int cost;
    public ItemRarity rarity;
    public ItemType type;

    public int healthPoints = 0;
    public int attack = 0;
    public float defense = 0;
    public float hitChance = 0;
    public float dodgeChance = 0;
    public float blockChance = 0;
    public float criticalChance = 0;
    public float criticalDamage = 0;
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
