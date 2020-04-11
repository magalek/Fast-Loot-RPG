using System.Collections.Generic;
using System.Linq;
using RPG.Events;
using RPG.Items.Slots;
using UnityEngine;

namespace RPG.Items
{
    public class Inventory : MonoBehaviour {

        public static Inventory Instance;

        [SerializeField] private GameObject slotPrefab;
        [SerializeField] private Transform gridTabsParent;

        public int slotCount;

        public void Start() {
            if (Instance == null)       
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);
        }
        
        public static void AddItem(Item itemToAdd, bool sendInventoryEvent = true) {

            InventorySlot firstEmptySlot = null;
            if (firstEmptySlot == null) 
                return;
            
            firstEmptySlot.InsertItem(itemToAdd);
            if (sendInventoryEvent)
                InventoryEvents.OnInventoryChange(itemToAdd.type);
        }

        public static void RemoveItem(Item itemToRemove, bool sendInventoryEvent = true) {

            InventorySlot inventorySlotOfItem = null;

            inventorySlotOfItem.RemoveItem();

            if (sendInventoryEvent)
                InventoryEvents.OnInventoryChange(itemToRemove.type);
        }
    }
}


