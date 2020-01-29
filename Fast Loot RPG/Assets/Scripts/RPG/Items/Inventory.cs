using System.Collections.Generic;
using System.Linq;
using RPG.Items.Slots;
using UnityEngine;

namespace RPG.Items
{
    public class Inventory : MonoBehaviour {

        public static Inventory Instance;

        [SerializeField] private GameObject slotPrefab;
        [SerializeField] private Transform gridTabsParent;

        public static List<ItemTab> ItemTabs = new List<ItemTab>();

        public int slotCount;

        //public static List<Item> allItemsInTab = new List<Item>();    

        private void Awake()
        {
            InventoryEventHandler.InventoryChange += SortItems;
        }

        public void Start()
        {
            if (Instance == null)       
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);
            InitialiseInventoryGrids();
        }

        private void InitialiseInventoryGrids()
        {
            ItemTabs = gridTabsParent.GetComponentsInChildren<ItemTab>().ToList();

            foreach (var tab in ItemTabs)
            {
                for (int j = 0; j < slotCount; j++)
                {
                    var inventorySlotGameObject = Instantiate(slotPrefab, tab.transform);
                    tab.inventorySlots.Add(inventorySlotGameObject.GetComponent<InventorySlot>());
                }
            }

            foreach (var tab in ItemTabs)
            {
                tab.gameObject.SetActive(false);
            }

            ItemTabs[0].gameObject.SetActive(true);
        }

        private static ItemTab GetItemTabFromType(ItemType itemType) 
            => ItemTabs.FirstOrDefault(i => i.type == itemType);

        public static void AddItem(Item itemToAdd, bool sendInventoryEvent = true)
        {
            ItemTab itemTab = GetItemTabFromType(itemToAdd.type);

            InventorySlot firstEmptySlot = itemTab.GetFirstEmptySlot();
            if (firstEmptySlot != null)
            {
                firstEmptySlot.HandleAddedItem(itemToAdd);
                if (sendInventoryEvent)
                    InventoryEventHandler.OnInventoryChange(itemToAdd.type);
            }
            else if (itemToAdd != null)
                Destroy(itemToAdd.gameObject);
        }

        public static void RemoveItem(Item itemToRemove, bool sendInventoryEvent = true)
        {
            ItemTab itemTab = GetItemTabFromType(itemToRemove.type);

            InventorySlot inventorySlotOfItem = itemTab.FindItemSlot(itemToRemove);

            inventorySlotOfItem.RemoveItem();

            if (sendInventoryEvent)
                InventoryEventHandler.OnInventoryChange(itemToRemove.type);
        }

        private static void SortItems(ItemType addedItemType)
        {
            List<Item> allItemsInTab = new List<Item>();

            ItemTab itemTab = GetItemTabFromType(addedItemType);

            foreach (var slot in itemTab.inventorySlots)
            {
                if (slot?.item)
                    allItemsInTab.Add(slot.item);
            }

            allItemsInTab = allItemsInTab.OrderByDescending(i => i?.ItemLevel).ToList();

            itemTab.RemoveItemsInTab();
            itemTab.AddItemsToTab(allItemsInTab);
        }
    }
}


