using RPG.Items;
using UnityEngine;

namespace RPG.Managers
{
    public class ItemManager : MonoBehaviour
    {
        public static ItemManager Instance;

        private int itemNum = 0;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(this);
        }

        public Item CreateNewItem(float legendaryChance = 0.4f)
        {
            Item randomItem = ResourceManager.itemPrefabs[Random.Range(0, ResourceManager.itemPrefabs.Length)];

            if (legendaryChance > Random.value)
                return GenerateItem(randomItem, ItemRarity.Legendary);
            else
                return GenerateItem(randomItem, ItemRarity.Common);
        }

        private Item GenerateItem(Item itemToGenerate, ItemRarity rarity)
        {
            Item item = Instantiate(itemToGenerate, transform);
            item.name = itemToGenerate.name + $" {itemNum}";
            item.rarity = rarity;
            itemNum++;
            return item;
        }

        
    }
}
