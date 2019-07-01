using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemTooltip : MonoBehaviour
{
    public static ItemTooltip Instance;

    [SerializeField] TextMeshProUGUI itemTooltipTextObject;

    static TextMeshProUGUI itemTooltipText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        itemTooltipText = itemTooltipTextObject;
    }

    public static void ChangeTooltip(string message) => itemTooltipText.text = message;
}
