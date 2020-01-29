using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Items;
using UnityEngine;

public class InventoryEventHandler {

    public static event Action<ItemType> InventoryChange;
    public static event Action<Item> ItemDestroyed;

    public static void OnInventoryChange(ItemType itemType) => InventoryChange?.Invoke(itemType);
    public static void OnItemDestroyed(Item item) => ItemDestroyed?.Invoke(item);
}
