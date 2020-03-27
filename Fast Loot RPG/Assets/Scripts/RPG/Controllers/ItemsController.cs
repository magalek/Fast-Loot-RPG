using RPG.Items;
using UnityEngine;

namespace RPG.Controllers
{
    public class ItemsController : MonoBehaviour {
        private static ItemObject InstantiateItemObjectAtPosition(Vector3 position, GameObject itemObjectPrefab = null) {
            if (itemObjectPrefab == null) {
                itemObjectPrefab = ResourcesController.itemObjectsPrefabs[Random.Range(0, ResourcesController.itemObjectsPrefabs.Length)];
            }

            ItemObject itemObject 
                = Instantiate(itemObjectPrefab, position, Quaternion.identity).GetComponent<ItemObject>();

            itemObject.SetPrefab(itemObjectPrefab);
            
            Item item = new Item(itemObject);
            itemObject.SetItem(item);
            return itemObject;
        }
        
        public static void DropItemAtPosition(Vector3 position, Item item = null) {
            if (item != null) {
                ItemObject itemObject = InstantiateItemObjectAtPosition(position, item.itemObjectPrefab);
                itemObject.IsRecentlyDropped(true);
            }
            else {
                InstantiateItemObjectAtPosition(position);
            }
        }
    }
}
