using RPG.Items;
using UnityEngine;

namespace RPG.Controllers
{
    public class ItemsController : MonoBehaviour
    {
        public static ItemsController Instance;

        private int amountOfItemsGenerated = 0;

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
            Item randomItem = ResourcesController.itemPrefabs[Random.Range(0, ResourcesController.itemPrefabs.Length)];

            if (legendaryChance > Random.value)
                return GenerateItem(randomItem, ItemRarity.Legendary);
            else
                return GenerateItem(randomItem, ItemRarity.Common);
        }

        private Item GenerateItem(Item itemToGenerate, ItemRarity rarity)
        {
            Item item = Instantiate(itemToGenerate, transform);
            item.name = itemToGenerate.name + $" {amountOfItemsGenerated}";
            item.rarity = rarity;
            amountOfItemsGenerated++;
            return item;
        }

        
    }
}
