using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour {

    public Item weaponSlot;
    public Item armorSlot;
    public Item shieldSlot;

    Entity entity;

    private void Awake()
    {
        entity = GetComponent<Entity>();
    }

    public void EquipItem(Item item)
    {
        switch (item.type)
        {
            case ItemType.Weapon:
                HandleCorrectSlot(ref weaponSlot, item);
                break;
            case ItemType.Armor:
                HandleCorrectSlot(ref armorSlot, item);
                break;
            case ItemType.Shield:
                HandleCorrectSlot(ref shieldSlot, item);
                break;
            default:
                break;
        }
    }

    public void EquipItem(int slotIndex)
    {
        if (Inventory.Instance.items.Count < slotIndex + 1)
            return;

        Item item = Inventory.Instance.items[slotIndex];

        switch (item.type)
        {
            case ItemType.Weapon:
                HandleCorrectSlot(ref weaponSlot, item);
                break;
            case ItemType.Armor:
                HandleCorrectSlot(ref armorSlot, item);
                break;
            case ItemType.Shield:
                HandleCorrectSlot(ref shieldSlot, item);
                break;
            default:
                break;
        }
    }

    public void UnequipItems()
    {
        if (weaponSlot != null)
        {
            entity.RemoveStatistics(entity, weaponSlot);
            Inventory.Instance.AddToInventory(weaponSlot);
            weaponSlot = null;
        }
        if (armorSlot != null)
        {
            entity.RemoveStatistics(entity, armorSlot);
            Inventory.Instance.AddToInventory(armorSlot);
            armorSlot = null;
        }
        if (shieldSlot != null)
        {
            entity.RemoveStatistics(entity, shieldSlot);
            Inventory.Instance.AddToInventory(shieldSlot);
            shieldSlot = null;
        }
    }

    public void UnequipItem(Item item)
    {
        entity.RemoveStatistics(entity, item);
        Inventory.Instance.AddToInventory(item);
    }

    private void HandleCorrectSlot(ref Item slot, Item itemToEquip)
    {
        if (slot == null)
        {
            slot = itemToEquip;
            Inventory.Instance.RemoveFromInventory(itemToEquip);                     
            entity.AddStatistics(entity, itemToEquip);
        }
        else
        {
            UnequipItem(slot);
            slot = itemToEquip;
            Inventory.Instance.RemoveFromInventory(itemToEquip);
            entity.AddStatistics(entity, itemToEquip);
        }
    }
}
