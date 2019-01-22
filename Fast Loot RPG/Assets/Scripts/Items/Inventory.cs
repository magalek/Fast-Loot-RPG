using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour {

    public static Inventory Instance;

    //public delegate void InventoryAction();
    //public static event InventoryAction OnItemAdded;
    //public static event InventoryAction OnItemRemoved;

    [SerializeField] GameObject slotPrefab;
    [SerializeField] Transform gridTransform;

    public int slotCount;

    public List<Item> itemList;
    public List<InventorySlot> inventorySlots;

    public void Start()
    {
        if (Instance == null)
            Instance = this;
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
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i].isEmpty)
                return inventorySlots[i];
        }
        return null;
    }

    public void AddToInventory(Item item)
    {
        InventorySlot firstEmptySlot = GetFirstEmptySlot();
        if (firstEmptySlot != null)
        {
            firstEmptySlot.HandleAddedItem(item);
        }
        //items = items.OrderByDescending(o => o.itemLevel).ToList();
    }

    public void RemoveFromInventory(InventorySlot inventorySlot)
    {
        inventorySlot.item = null;
        inventorySlot.HandleRemovedItem();
    }
}
