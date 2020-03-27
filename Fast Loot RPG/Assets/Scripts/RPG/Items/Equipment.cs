using RPG.Entities;
using RPG.Events;
using RPG.Items.Slots;
using UnityEngine;

namespace RPG.Items
{
    public class Equipment : MonoBehaviour
    {
        //public static Equipment Instance;

        [SerializeField] Transform equipmentGridTransform;

        static EquipmentSlot[] equipmentSlots;

        static Entity owner;

        private void Awake() {
            // if (Instance == null)
            //     Instance = this;
            // else if (Instance != this)
            //     Destroy(gameObject);

            owner = GetComponentInParent<Player>();

            InitializeEquipmentSlots();
        }

        private void InitializeEquipmentSlots() {
            for (int i = 0; i < equipmentGridTransform.childCount; i++)
                equipmentSlots = equipmentGridTransform.GetComponentsInChildren<EquipmentSlot>();
        }

        public static bool EquipItem(Item item) {
            EquipmentSlot correctSlot = GetCorrectSlot(item);
            if (correctSlot && correctSlot.isEmpty) {
                correctSlot.InsertItem(item);
                owner.stats += item.stats;           
                item.IsEquipped = true;
                InventoryEvents.OnInventoryChange(item.type);
                return true;
            }
            return false;
        }

        public static void UnequipItem(Item item, EquipmentSlot equipmentSlot) {
            owner.stats -= item.stats;
            Inventory.AddItem(item, false);
            equipmentSlot.item = null;        
            item.IsEquipped = false;
            InventoryEvents.OnInventoryChange(item.type);
        }

        private static EquipmentSlot GetCorrectSlot(Item itemToEquip) {
            foreach (EquipmentSlot slot in equipmentSlots)
                if (slot.slotItemType == itemToEquip.type)
                    return slot;
            return null;
        }
    }
}
