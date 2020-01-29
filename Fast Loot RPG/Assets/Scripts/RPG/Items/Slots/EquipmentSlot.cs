using UnityEngine.UI;

namespace RPG.Items.Slots
{
    public class EquipmentSlot : Slot {

        public ItemType slotItemType;

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
}
