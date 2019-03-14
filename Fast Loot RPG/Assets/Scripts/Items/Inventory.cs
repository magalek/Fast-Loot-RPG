using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour {

    public static Inventory Instance;

    [SerializeField] GameObject slotPrefab;
    [SerializeField] Transform gridTransform;

    public int slotCount;

    public List<Item> allItems;
    public List<InventorySlot> inventorySlots;

    private void Awake()
    {
        InventoryEventHandler.InventoryChange += SortItems;
    }

    public void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        InitializeInventoryGrid();
    }

    private void InitializeInventoryGrid()
    {
        for (int i = 0; i < slotCount; i++)
        {
            var inventorySlotGameObject = Instantiate(slotPrefab, gridTransform);
            inventorySlots.Add(inventorySlotGameObject.GetComponent<InventorySlot>());
        }
    }

    InventorySlot GetFirstEmptySlot()
    {
        return inventorySlots.FirstOrDefault(s => s.isEmpty);
    }

    public void AddItem(Item item)
    {
        InventorySlot firstEmptySlot = GetFirstEmptySlot();
        if (firstEmptySlot != null)
            firstEmptySlot.HandleAddedItem(item);
        else if (item != null)
            Destroy(item.gameObject);
    }

    public void RemoveItem(InventorySlot inventorySlot)
    {
        inventorySlot.item = null;
        inventorySlot.HandleRemovedItem();
    }

    public void SortItems()
    {
        foreach (var slot in inventorySlots)
        {
            allItems.Add(slot.item);            
        }

        allItems = allItems.OrderByDescending(i => i?.itemLevel).ToList();

        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i] != null)
                inventorySlots[i].HandleRemovedItem();

            if (allItems[i] != null)
                inventorySlots[i].HandleAddedItem(allItems[i]);
        }
        allItems.Clear();
    }
}


