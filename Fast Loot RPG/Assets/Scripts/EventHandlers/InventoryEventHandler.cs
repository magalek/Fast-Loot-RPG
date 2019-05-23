using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryEventHandler {

    public delegate void InventoryDelegate(Item item);

    public static event InventoryDelegate InventoryChange;

    public static void OnInventoryChange(Item item) => InventoryChange?.Invoke(item);
}
