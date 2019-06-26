using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : Slot {

    Button slotButton;

    private void Awake()
    {
        //slotButton = GetComponent<Button>();
        //slotButton.onClick.AddListener(SlotClick);
        isEmpty = true;
        item = null;
    }

    public override void SlotLeftButtonClick()
    {
        if (item != null)
        {
            if (Equipment.EquipItem(item))
                Inventory.RemoveItemFromSlot(this);
        }
    }

    public override void SlotRightButtonClick()
    {
        if (item != null)
        {
            Inventory.RemoveItemFromSlot(this);
            Destroy(item);
        }
    }
}