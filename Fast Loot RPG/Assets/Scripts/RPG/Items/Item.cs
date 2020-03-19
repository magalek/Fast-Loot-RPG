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
        public event Action ItemEquipped;
        public event Action ItemUnequipped;

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
        public Statistics statistics;

        public ItemRarity rarity;
        public ItemType type;

        public int ItemLevel;
        public bool IsEquipped;

        public Item(ItemObject itemObject) {
            itemObjectPrefab = itemObject.prefab;
            statistics = itemObject.statRanges.GetRandomStats();
            sprite = itemObject.GetComponentInChildren<SpriteRenderer>().sprite;
            type = itemObject.type;
            CalculateItemLevel(); 
        }

        public void OnItemEquipped() => ItemEquipped?.Invoke(); 
        public void OnItemUnequipped() => ItemUnequipped?.Invoke();

        private void CalculateItemLevel()
        {
            ItemLevel += statistics.healthPoints;
            ItemLevel += statistics.attack;
        }

        //TODO: Waiting for legendary items feature
        // public void AddLegendaryAbility()
        // {
        //     switch (type)
        //     {
        //         case ItemType.Sword:
        //             gameObject.AddComponent<LegendarySwordAbility>();                
        //             break;
        //         case ItemType.Mace:
        //             gameObject.AddComponent<LegendaryMaceAbility>();
        //             break;
        //         case ItemType.Dagger:
        //             break;
        //         case ItemType.Armor:
        //             gameObject.AddComponent<LegendaryArmorAbility>();
        //             break;
        //         case ItemType.Helmet:
        //             break;
        //         case ItemType.Pants:
        //             break;
        //         case ItemType.Shield:
        //             break;
        //         default:
        //             break;
        //     }
        // }
    }

    public enum ItemRarity
    {
        Common,
        Legendary
    }

    public enum ItemType
    {
        Sword,
        Mace,
        Dagger,
        Armor,
        Helmet,
        Pants,
        Shield,
        Other
    }
}