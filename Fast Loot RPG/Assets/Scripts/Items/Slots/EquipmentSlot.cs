using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlot : Slot {

    public ItemType slotItemType;

    Button slotButton;

    private void Awake()
    {
        isEmpty = true;
        item = null;
    }

    public override void SlotLeftButtonClick()
    {
        if (item != null)
        {
            Equipment.UnequipItem(item, this);
            RemoveItem();
        }
    }
}
