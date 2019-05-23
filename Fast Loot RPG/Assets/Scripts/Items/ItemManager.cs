using UnityEngine;

public class ItemManager : MonoBehaviour
{

    public static ItemManager Instance;

    [SerializeField] Item[] itemPrefabs;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(this);
        InitializeItemDatabase();
    }

    public Item CreateNewItem(float legendaryChance = 0.4f)
    {
        Item randomItem = itemPrefabs[Random.Range(0, itemPrefabs.Length)];

        if (legendaryChance > Random.value)
        {
            Item item = GenerateItem(randomItem, ItemRarity.Legendary);
            item.AddLegendaryAbility();
            return item;
        }
        else
        {
            Item item = GenerateItem(randomItem, ItemRarity.Common);
            return item;
        }
    }

    Item GenerateItem(Item itemToGenerate, ItemRarity rarity)
    {
        Item item = Instantiate(itemToGenerate, transform);
        item.name = itemToGenerate.name;
        item.rarity = rarity;
        return item;
    }

    private void InitializeItemDatabase() => itemPrefabs = Resources.LoadAll<Item>("Prefabs/Item Prefabs");
}
