using System;
using UnityEngine;

namespace RPG.Items {
    public class ItemObject : MonoBehaviour {
        
        public Item item;

        public GameObject prefab;
        
        public ItemStats statRanges;
        public ItemType type;

        private bool recentlyDropped = false;
        public void SetItem(Item itemToSet) => item = itemToSet;
        public void SetPrefab(GameObject prefabToSet) => prefab = prefabToSet;
        public void IsRecentlyDropped(bool recentlyDropped) => this.recentlyDropped = recentlyDropped;
        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player") && !recentlyDropped) {
                Inventory.AddItem(item);
                Destroy(gameObject);
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (other.CompareTag("Player")){
                IsRecentlyDropped(false);
            }
        }
    }
}