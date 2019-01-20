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
            item.rarity = Rarity.Legendary;
            return item;
        }
        else
        {
            Item item = items[Random.Range(0, items.Length)];
            return item;
        }
    }
}
