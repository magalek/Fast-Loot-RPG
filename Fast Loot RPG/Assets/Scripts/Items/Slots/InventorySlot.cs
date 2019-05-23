using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : Slot {

    Button slotButton;

    private void Awake()
    {
        slotButton = GetComponent<Button>();
        slotButton.onClick.AddListener(SlotClick);
        isEmpty = true;
        item = null;
    }

    public override void SlotClick()
    {
        if (item != null)
        {
            if (Equipment.EquipItem(item))
                Inventory.RemoveItem(this);
        }
    }
}