using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour {

    public static Inventory Instance;

    [SerializeField] GameObject slotPrefab;
    [SerializeField] Transform gridTabsParent;

    public static List<ItemTab> itemTabs = new List<ItemTab>();

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
        InitializeInventoryGrids();
    }

    private void InitializeInventoryGrids()
    {
        itemTabs = gridTabsParent.GetComponentsInChildren<ItemTab>().ToList();

        for (int i = 0; i < itemTabs.Count; i++)
        {
            for (int j = 0; j < slotCount; j++)
            {
                var inventorySlotGameObject = Instantiate(slotPrefab, itemTabs[i].transform);
                itemTabs[i].inventorySlots.Add(inventorySlotGameObject.GetComponent<InventorySlot>());
            }
        }

        foreach (var tab in itemTabs)
        {
            tab.gameObject.SetActive(false);
        }

        itemTabs[0].gameObject.SetActive(true);
    }    

    static ItemTab GetProperItemTab(ItemType itemType) 
        => itemTabs.FirstOrDefault(i => i.type == itemType);

    public static void AddItem(Item addedItem, bool sendInventoryEvent = true)
    {
        ItemTab itemTab = GetProperItemTab(addedItem.type);

        InventorySlot firstEmptySlot = itemTab.GetFirstEmptySlot();
        if (firstEmptySlot != null)
        {
            firstEmptySlot.HandleAddedItem(addedItem);
            if (sendInventoryEvent)
                InventoryEventHandler.OnInventoryChange(addedItem.type);
        }
        else if (addedItem != null)
            Destroy(addedItem.gameObject);
    }

    public static void RemoveItem(Item removedItem, bool sendInventoryEvent = true)
    {
        ItemTab itemTab = GetProperItemTab(removedItem.type);

        InventorySlot inventorySlot = itemTab.FindItemSlot(removedItem);

        inventorySlot.item = null;
        inventorySlot.HandleRemovedItem();

        if (sendInventoryEvent)
            InventoryEventHandler.OnInventoryChange(removedItem.type);
    }  

    public static void SortItems(ItemType addedItemType)
    {
        List<Item> allItemsInTab = new List<Item>();

        ItemTab itemTab = GetProperItemTab(addedItemType);

        foreach (var slot in itemTab.inventorySlots)
        {
            if (slot?.item)
                allItemsInTab.Add(slot.item);
            else continue;
        }

        allItemsInTab = allItemsInTab.OrderByDescending(i => i?.itemLevel).ToList();

        itemTab.RemoveItemsInTab();
        itemTab.AddItemsToTab(allItemsInTab);
    }
}


