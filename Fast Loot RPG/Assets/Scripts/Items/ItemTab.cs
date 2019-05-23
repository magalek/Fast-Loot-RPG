using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ItemTab : MonoBehaviour
{
    [SerializeField] Button tabButton;

    public ItemType type;
    public List<InventorySlot> inventorySlots = new List<InventorySlot>();
    public InventorySlot GetFirstEmptySlot() => inventorySlots.FirstOrDefault(s => s.isEmpty);

    private void Awake()
    {
        tabButton.onClick.AddListener(ChangeTab);
    }

    void ChangeTab()
    {
        foreach (var tab in Inventory.itemTabs)
        {
            tab.gameObject.SetActive(false);
        }

        gameObject.SetActive(true);

        InventoryUI.ResetSlider();
        InventoryUI.ChangeScrollRectContent(GetComponent<RectTransform>());
    }
}
