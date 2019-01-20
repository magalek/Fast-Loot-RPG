using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public static Inventory Instance;

    public int slotCount;

    public List<Item> items;

    public void Start()
    {
        if (Instance == null)
            Instance = this;        
    }

    public void AddToInventory(Item item)
    {
        if (items.Count <= slotCount)
            items.Add(item);
    }
}
