using System.Collections;
using System.Collections.Generic;
using RPG.Events;
using TMPro;
using UnityEngine;

public class ItemTooltip : MonoBehaviour
{
    public static ItemTooltip Instance;

    [SerializeField] TextMeshProUGUI itemTooltipTextObject;

    static TextMeshProUGUI itemTooltipText;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        itemTooltipText = itemTooltipTextObject;
        InventoryEvents.InventoryChange += i => ChangeTooltip(""); 
        gameObject.SetActive(false);
    }

    public static void ChangeTooltip(string message) => itemTooltipText.text = message;
}
