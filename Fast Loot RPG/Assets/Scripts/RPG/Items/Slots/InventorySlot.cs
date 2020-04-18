using RPG.Entities;

namespace RPG.Items.Slots
{
    public class InventorySlot : Slot {
        
        private Inventory inventory;

        private void Awake() {
            base.Awake();
            inventory = GetComponentInParent<Inventory>();
        }
        
        protected override void SlotLeftButtonClick() {
            if (item == null) return;

            if (Player.Instance.equipment.Equip(item))
                RemoveItem();
        }
        
        protected override void SlotRightButtonClick() {
            inventory.Remove(item);
        }
    }
}