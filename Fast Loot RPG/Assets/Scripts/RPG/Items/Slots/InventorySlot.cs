namespace RPG.Items.Slots
{
    public class InventorySlot : Slot {
        private void Awake()
        {
            isEmpty = true;
            item = null;
        }

        public override void SlotLeftButtonClick()
        {
            if (item != null)
                if (Equipment.EquipItem(item))
                    Inventory.RemoveItem(item);
        }
        //TODO: fix the same item going into two slots
        public override void SlotRightButtonClick()
        {
            if (item == null) 
                return;

            Inventory.RemoveItem(item);
            Destroy(item.gameObject);

            ItemTooltip.ChangeTooltip("");
        }
    }
}