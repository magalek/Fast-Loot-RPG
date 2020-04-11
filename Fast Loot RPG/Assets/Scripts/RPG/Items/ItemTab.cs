using System.Collections.Generic;
using System.Linq;
using RPG.Items.Slots;
using RPG.UI;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Items {
    public class ItemTab : MonoBehaviour {
        [SerializeField] Button tabButton;

        public ItemType type;
        public List<InventorySlot> inventorySlots = new List<InventorySlot>();

        private void Awake() {
            tabButton.onClick.AddListener(Select);
        }
        
        public InventorySlot GetFirstEmptySlot() => inventorySlots.FirstOrDefault(s => s.isEmpty);
        

        public InventorySlot GetItemSlot(Item itemInSlot) {
            return inventorySlots.FirstOrDefault(slot => slot.item == itemInSlot);
        }
        private void Select() {
            foreach (var tab in Inventory.ItemTabs)
            {
                tab.gameObject.SetActive(false);
            }

            gameObject.SetActive(true);

            InventoryUI.ResetSlider();
            InventoryUI.ChangeScrollRectContent(GetComponent<RectTransform>());
        }
    }
}
