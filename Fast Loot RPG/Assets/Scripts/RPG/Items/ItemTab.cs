using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RPG.Items;
using RPG.Items.Slots;
using UnityEngine;
using UnityEngine.UI;

public class ItemTab : MonoBehaviour
{
    [SerializeField] Button tabButton;

    public ItemType type;
    public List<InventorySlot> inventorySlots = new List<InventorySlot>();

    public InventorySlot GetFirstEmptySlot() => inventorySlots.FirstOrDefault(s => s.isEmpty);

    private void Awake()
    {
        tabButton.onClick.AddListener(ChangeTab);
    }

    public void RemoveItemsInTab()
    {
        foreach (var slot in inventorySlots)
        {
            if (slot.item != null)
                Inventory.RemoveItem(slot.item, false);
        }
    }

    public void AddItemsToTab(List<Item> itemsToAdd)
    {
        foreach (var item in itemsToAdd)
        {
            Inventory.AddItem(item, false);
        }
    }

    public InventorySlot FindItemSlot(Item itemInSlot)
    {
        foreach (var slot in inventorySlots)
        {
            if (slot.item == itemInSlot)
                return slot;
        }
        return null;
    }

    private void ChangeTab()
    {
        foreach (var tab in Inventory.ItemTabs)
        {
            tab.gameObject.SetActive(false);
        }

        gameObject.SetActive(true);

        InventoryUI.ResetSlider();
        InventoryUI.ChangeScrollRectContent(GetComponent<RectTransform>());
    }
}
