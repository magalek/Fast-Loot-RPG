using System;
using RPG.Items;

namespace RPG.Events {
    public class InventoryEvents {

        public static event Action<ItemType> InventoryChange;
        public static event Action<Item> ItemDestroyed;

        public static void OnInventoryChange(ItemType itemType) => InventoryChange?.Invoke(itemType);
        public static void OnItemDestroyed(Item item) => ItemDestroyed?.Invoke(item);
    }
}
