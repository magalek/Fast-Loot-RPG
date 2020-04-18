using System;
using RPG.Entities;
using UnityEngine;

namespace RPG.Items {
    public class ItemObject : MonoBehaviour {
        
        public Item item = null;

        public GameObject prefab;
        
        public ItemType type;

        public bool recentlyDropped;

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player") && other.isTrigger && !recentlyDropped) {
                Player.Instance.inventory.Add(item);
                Destroy(gameObject);
            }
        }
        private void OnTriggerExit2D(Collider2D other) {
            if (other.CompareTag("Player")){
                recentlyDropped = false;
            }
        }
    }
}