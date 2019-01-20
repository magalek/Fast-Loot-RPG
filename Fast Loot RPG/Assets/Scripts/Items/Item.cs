using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

    public int cost;
    public Rarity rarity;

}

public enum Rarity
{
    Common,
    Legendary
}

