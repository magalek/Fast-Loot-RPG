using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour {

    public static Equipment Instance;

    [SerializeField] Transform equipmentGridTransform;

    EquipmentSlot[] equipmentSlots;

    Player player;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        player = FindObjectOfType<Player>();

        InitializeEquipmentSlots();
    }

    private void InitializeEquipmentSlots()
    {
        for (int i = 0; i < equipmentGridTransform.childCount; i++)
        {
            equipmentSlots = equipmentGridTransform.GetComponentsInChildren<EquipmentSlot>();
        }
    }

    public bool EquipItem(Item item)
    {
        EquipmentSlot correctSlot = GetCorrectSlot(item);
        if (correctSlot.isEmpty)
        {
            correctSlot.HandleAddedItem(item);
            player.AddStatistics(player, item);
            return true;
        }
        return false;
    }

    public void UnequipItem(Item item, EquipmentSlot equipmentSlot)
    {
        player.RemoveStatistics(player, item);
        Inventory.Instance.AddToInventory(item);
        equipmentSlot.item = null;
    }

    private EquipmentSlot GetCorrectSlot(Item itemToEquip)
    {
        foreach (EquipmentSlot slot in equipmentSlots)
        {
            if (slot.slotItemType == itemToEquip.type)
                return slot;
        }
        return null;
    }
}
