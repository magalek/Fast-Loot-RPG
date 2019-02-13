using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour {

    public static Inventory Instance;

    [SerializeField] GameObject slotPrefab;
    [SerializeField] Transform gridTransform;

    public int slotCount;

    public List<Item> itemList;
    public List<InventorySlot> inventorySlots;

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

    public void AddToInventory(Item item)
    {
        InventorySlot firstEmptySlot = GetFirstEmptySlot();
        if (firstEmptySlot != null)
            firstEmptySlot.HandleAddedItem(item);
        else
            Destroy(item.gameObject);
        //items = items.OrderByDescending(o => o.itemLevel).ToList();
    }

    public void RemoveFromInventory(InventorySlot inventorySlot)
    {
        inventorySlot.item = null;
        inventorySlot.HandleRemovedItem();
    }
}


