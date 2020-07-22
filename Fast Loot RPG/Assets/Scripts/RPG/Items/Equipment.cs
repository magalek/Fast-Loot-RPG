using System.Collections.Generic;
using System.Linq;
using RPG.Items.Slots;
using UnityEngine;

namespace RPG.Items
{
    public class Equipment : MonoBehaviour
    {
        private List<EquipmentSlot> slots;
        
        private void Awake() {
            slots = GetComponentsInChildren<EquipmentSlot>().ToList();
        }

        public bool Add(Item item) {
            EquipmentSlot slot = slots.FirstOrDefault(s => s.itemType == item.type);

            if (slot == null || !slot.isEmpty) return false;

            slot.Insert(item);
            return true;
        }

        public void Remove(Item item) {
            EquipmentSlot slot = slots.FirstOrDefault(s => s.item == item);

            if (slot == null) return;

            slot.RemoveItem();
        }
    }
}
