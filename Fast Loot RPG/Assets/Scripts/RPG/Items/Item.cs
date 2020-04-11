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
        public GameObject itemObjectPrefab;
        public Sprite sprite;
        public ItemType type;

        public bool isEquipped;

        public Item(ItemObject itemObject) {
            itemObjectPrefab = itemObject.prefab;
            sprite = itemObject.GetComponentInChildren<SpriteRenderer>().sprite;
            type = itemObject.type;
        }
    }
}