using UnityEngine;

public class Equipment : MonoBehaviour
{

    public static Equipment Instance;

    [SerializeField] Transform equipmentGridTransform;

    static EquipmentSlot[] equipmentSlots;

    static Player player;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

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

    public static bool EquipItem(Item item)
    {
        EquipmentSlot correctSlot = GetCorrectSlot(item);
        if (correctSlot && correctSlot.isEmpty)
        {
            correctSlot.HandleAddedItem(item);
            player.statistics += item.statistics;
            InventoryEventHandler.OnInventoryChange(item);
            item.equipped = true;
            item.OnItemEquipped();
            return true;
        }
        return false;
    }

    public static void UnequipItem(Item item, EquipmentSlot equipmentSlot)
    {
        player.statistics -= item.statistics;
        Inventory.AddItem(item);
        equipmentSlot.item = null;
        InventoryEventHandler.OnInventoryChange(item);
        item.equipped = false;
        item.OnItemUnequipped();
    }

    static EquipmentSlot GetCorrectSlot(Item itemToEquip)
    {
        foreach (EquipmentSlot slot in equipmentSlots)
        {
            if (slot.slotItemType == itemToEquip.type)
                return slot;
        }
        return null;
    }
}
