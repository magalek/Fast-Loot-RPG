namespace RPG.Items.Slots
{
    public class InventorySlot : Slot {
        
        private Inventory inventory;

        private void Awake() {
            base.Awake();
            inventory = GetComponentInParent<Inventory>();
        }
        
        // protected override void OnLeftMouseClick() {
        //     if (item == null) return;
        //
        //     if (Player.Instance.equipment.Add(item))
        //         RemoveItem();
        // }
        
        // protected override void OnRightMouseClick() {
        //     inventory.Remove(item);
        // }
    }
}