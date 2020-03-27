using System;
using RPG.Entities;
using UnityEngine.UI;

namespace RPG.Items.Slots
{
    public class EquipmentSlot : Slot  {

        public ItemType slotItemType;

        protected override void SlotLeftButtonClick() {
            if (item == null) 
                return;
            
            Equipment.UnequipItem(item, this);
            RemoveItem();
        }
    }
}
