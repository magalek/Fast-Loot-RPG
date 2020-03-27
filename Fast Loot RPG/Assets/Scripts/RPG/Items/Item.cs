using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Entities;
using UnityEngine;

namespace RPG.Items
{
    [Serializable]
    public class Item
    {

        //public Sprite sprite;
        public Color color { get
        {
            switch (rarity)
            {
                case ItemRarity.Common:
                    return Color.green;
                case ItemRarity.Legendary:
                    return new Color(1f, 0.45f, 0f);
                default:
                    return Color.white;
            }
        }}

        public GameObject itemObjectPrefab;

        public Sprite sprite;
        
        [Header("Stats")]
        public Stats stats;

        public ItemRarity rarity;
        public ItemType type;

        public int ItemLevel;
        public bool IsEquipped;

        public Item(ItemObject itemObject) {
            itemObjectPrefab = itemObject.prefab;
            stats = itemObject.statRanges.GetRandomStats();
            sprite = itemObject.GetComponentInChildren<SpriteRenderer>().sprite;
            type = itemObject.type;
        }
    }
}