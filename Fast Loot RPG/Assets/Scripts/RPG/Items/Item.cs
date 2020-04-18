using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Entities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RPG.Items
{
    [Serializable]
    public class Item {
        public GameObject prefab;
        
        public Sprite sprite;
        public ItemType type;

        public int value;
        public Item(ItemObject itemObject) {
            prefab = itemObject.prefab;
            sprite = itemObject.GetComponentInChildren<SpriteRenderer>().sprite;
            type = itemObject.type;
            value = Random.Range(10, 100);
        }

        public override string ToString() {
            return
                $"{prefab.name} \t {type} \n" +
                "\n" +
                "\n" +
                "\n" +
                $"Sell for: {value}";
        }
    }
}