using UnityEngine;

public class ItemManager : MonoBehaviour
{

    public static ItemManager Instance;

    [SerializeField] Item[] itemPrefabs;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(this);
        InitializeItemDatabase();
    }

    private void InitializeItemDatabase() => itemPrefabs = Resources.LoadAll<Item>("Prefabs/Item Prefabs");

    public Item GenerateItem(float legendaryChance = 0.4f)
    {
        Item randomItemFromDatabase = itemPrefabs[Random.Range(0, itemPrefabs.Length)];

        if (legendaryChance > Random.value)
        {
            Item item = Instantiate(randomItemFromDatabase, transform);
            item.name = randomItemFromDatabase.name;
            item.rarity = ItemRarity.Legendary;
            item.AddLegendaryAbility();
            return item;
        }
        else
        {
            Item item = Instantiate(randomItemFromDatabase, transform);
            item.name = randomItemFromDatabase.name;
            item.rarity = ItemRarity.Common;
            return item;
        }
    }
}
