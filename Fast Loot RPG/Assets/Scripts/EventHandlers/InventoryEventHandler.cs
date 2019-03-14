using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryEventHandler {

    public delegate void InventoryDelegate();

    public static event InventoryDelegate InventoryChange;

    public static void OnInventoryChange()
    {
        InventoryChange?.Invoke();
    }

}
