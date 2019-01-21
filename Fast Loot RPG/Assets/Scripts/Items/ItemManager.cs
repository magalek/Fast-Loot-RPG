using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {

    public static ItemManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    [SerializeField] Item[] items;

    public Item GenerateItem(float legendaryChance = 0.4f)
    {
        if (legendaryChance > Random.value)
        {
            Item item = items[Random.Range(0, items.Length)];
            //item.CalculateItemLevel();
            item.rarity = ItemRarity.Legendary;
            return item;
        }
        else
        {
            Item item = items[Random.Range(0, items.Length)];
            //item.CalculateItemLevel();
            return item;
        }
    }
}
