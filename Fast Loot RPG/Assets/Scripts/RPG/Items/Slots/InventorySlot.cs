﻿using RPG.Controllers;
using RPG.Entities;

namespace RPG.Items.Slots
{
    public class InventorySlot : Slot {
        
        protected override void SlotLeftButtonClick() {
            if (item == null) return;
            
            if (Equipment.EquipItem(item))
                Inventory.RemoveItem(item);
        }
        
        //TODO: fix the same item going into two slots
        protected override void SlotRightButtonClick() {
            if (item == null) return;

            ItemsController.DropItemAtPosition(Player.Instance.transform.position, item);
            Inventory.RemoveItem(item);
            //Destroy(item.gameObject);

            ItemTooltip.ChangeTooltip("");
        }
    }
}