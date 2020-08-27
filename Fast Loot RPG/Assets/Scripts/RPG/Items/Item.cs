using System;
using UnityEngine;
using CharacterInfo = RPG.Statistics.CharacterInfo;
using Random = UnityEngine.Random;

namespace RPG.Items
{
    [Serializable]
    public class Item {
        public GameObject prefab;
        
        public Sprite sprite;
        public ItemType type;

        public int value;
        public Item(ItemObject itemObject, CharacterInfo info) {
            prefab = itemObject.prefab;
            sprite = itemObject.GetComponentInChildren<SpriteRenderer>().sprite;
            type = itemObject.type;

            value = info.Damage.Current + info.Health.Max;

            value = Random.Range(value / 2, value);
        }

        public override string ToString() {
            return
                $"{prefab.name}\n\n" +
                $"Sell for: {value}";
        }
    }
}