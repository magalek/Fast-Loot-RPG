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

    public static List<Item> allItems = new List<Item>();
    //public static List<InventorySlot> inventorySlots = new List<InventorySlot>();

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

    public static void AddItem(Item item)
    {
        ItemTab itemTab = GetProperItemTab(item.type);

        InventorySlot firstEmptySlot = itemTab.GetFirstEmptySlot();
        if (firstEmptySlot != null)
            firstEmptySlot.HandleAddedItem(item);
        else if (item != null)
            Destroy(item.gameObject);
    }

    public static void RemoveItem(InventorySlot inventorySlot)
    {
        inventorySlot.item = null;
        inventorySlot.HandleRemovedItem();
    }

    public static void SortItems(Item addedItem)
    {
        if (addedItem)
        {
            ItemTab itemTab = GetProperItemTab(addedItem.type);

            foreach (var slot in itemTab.inventorySlots)
            {
                allItems.Add(slot.item);
            }

            allItems = allItems.OrderByDescending(i => i?.itemLevel).ToList();

            for (int i = 0; i < itemTab.inventorySlots.Count; i++)
            {
                if (itemTab.inventorySlots[i] != null)
                    itemTab.inventorySlots[i].HandleRemovedItem();

                if (allItems[i] != null)
                    itemTab.inventorySlots[i].HandleAddedItem(allItems[i]);
            }
            allItems.Clear();
        }
    }
}


