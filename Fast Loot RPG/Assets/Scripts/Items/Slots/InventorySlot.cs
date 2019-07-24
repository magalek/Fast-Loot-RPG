using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : Slot {

    Button slotButton;

    private void Awake()
    {
        isEmpty = true;
        item = null;
    }

    public override void SlotLeftButtonClick()
    {
        if (item != null)
            if (Equipment.EquipItem(item))
                Inventory.RemoveItem(item);
    }
    //TODO: fix the same item going into two slots
    public override void SlotRightButtonClick()
    {
        if (item != null)
        {
            GameObject itemGameObject = item.gameObject;

            Inventory.RemoveItem(item);
            Destroy(itemGameObject);

            ItemTooltip.ChangeTooltip("");
        }
    }
}