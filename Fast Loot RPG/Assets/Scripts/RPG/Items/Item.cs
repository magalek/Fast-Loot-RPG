using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Items
{
    //TODO: Refactor this class
    public class Item : MonoBehaviour
    {
        public event Action ItemEquipped;
        public event Action ItemUnequipped;

        public Sprite sprite;
        public Color color { get
        {
            switch (rarity)
            {
                case ItemRarity.Common:
                    return Color.white;
                case ItemRarity.Legendary:
                    return Color.magenta;
                default:
                    return Color.white;
            }
        }}

        [Header("Stats")]

        public ItemStatistics statRanges;
        public Statistics statistics;

        public ItemRarity rarity;
        public ItemType type;

        public int ItemLevel;

        public bool Equipped;

        private void Awake()
        {
            statistics = statRanges.GetRandomStats();
            CalculateItemLevel(); 
        }

        public void OnItemEquipped() => ItemEquipped?.Invoke();
        public void OnItemUnequipped() => ItemUnequipped?.Invoke();

        public void CalculateItemLevel()
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
        Shield
    }
}