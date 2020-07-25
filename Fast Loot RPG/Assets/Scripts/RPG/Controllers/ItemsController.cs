using System.Collections.Generic;
using RPG.Items;
using RPG.Utility;
using UnityEngine;

namespace RPG.Controllers
{
    public class ItemsController : MonoBehaviour {
        
        public static ItemsController Instance;

        private Transform itemsParent;
        
        private List<ItemObject> itemObjects = new List<ItemObject>();
        
        private void Awake() {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);
            
            itemsParent = new GameObject("Items").transform;
        }

        public void ClearItems() {
            foreach (ItemObject itemObject in itemObjects) {
                if (itemObject != null) Destroy(itemObject.gameObject);   
            }
            itemObjects.Clear();
        }

        public void CreateItemObject(Vector3 position) {
            GameObject itemPrefab = ResourcesController.itemObjectsPrefabs.Random();
            
            ItemObject itemObject = Instantiate(itemPrefab, position, Quaternion.identity).
                GetComponent<ItemObject>();
            itemObjects.Add(itemObject);
            itemObject.transform.SetParent(itemsParent);
            itemObject.prefab = itemPrefab;
            itemObject.item = new Item(itemObject);
        }
        
        public void CreateItemObject(Item item, Vector3 position) {
            ItemObject itemObject = Instantiate(item.prefab, position, Quaternion.identity).
                GetComponent<ItemObject>();
            itemObjects.Add(itemObject);
            itemObject.transform.SetParent(itemsParent);
            itemObject.item = item;
            itemObject.recentlyDropped = true;
        }
    }
}
