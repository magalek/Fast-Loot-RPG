using System;
using RPG.Entities;
using UnityEngine.UI;

namespace RPG.Items.Slots
{
    public class EquipmentSlot : Slot  {

        public ItemType itemType;
        private Equipment equipment;

        private void Awake() {
            base.Awake();
            equipment = GetComponentInParent<Equipment>();
        }
        
        protected override void SlotLeftButtonClick() {
            if (item == null) 
                return;
            Player.Instance.inventory.Add(item);
            equipment.Unequip(item);
            RemoveItem();
        }
    }
}
