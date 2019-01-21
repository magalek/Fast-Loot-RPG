using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour {

    public static Inventory Instance;

    [SerializeField] GameObject slotPrefab;
    [SerializeField] Transform gridTransform;

    public int slotCount;

    public List<Item> items;

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
            Instantiate(slotPrefab, gridTransform);
        }
    }

    public void AddToInventory(Item item)
    {
        if (items.Count <= slotCount)
            items.Add(item);
        items = items.OrderByDescending(o => o.itemLevel).ToList();
    }

    public void RemoveFromInventory(Item item)
    {
        items.Remove(item);
        items = items.OrderByDescending(o => o.itemLevel).ToList();
    }
}
