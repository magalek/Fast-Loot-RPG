using System.Collections.Generic;
using System.Linq;
using RPG.Entities;
using RPG.Utility;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace RPG.Items {
    public class ItemObject : MonoBehaviour {

        [SerializeField] private List<Sprite> availableSprites;
        
        public Item item = null;

        public GameObject prefab;
        
        public ItemType type;

        public bool recentlyDropped;

        private void Awake() {
            SetRandomSprite();
            ChangeLightColor();
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player") && other.isTrigger && !recentlyDropped && !Player.Instance.inventory.isFull) {
                Player.Instance.inventory.Add(item);
                Destroy(gameObject);
            }
        }
        private void OnTriggerExit2D(Collider2D other) {
            if (other.CompareTag("Player")){
                recentlyDropped = false;
            }
        }

        private void SetRandomSprite() {
            GetComponentInChildren<SpriteRenderer>().sprite = availableSprites.Random();
        }

        private void ChangeLightColor() {
            Sprite sprite = GetComponentInChildren<SpriteRenderer>().sprite;
            Texture2D texture = sprite.texture;
            Vector4 spriteMean = new Vector4(0,0,0,1);
            Color[] allPixels = texture.GetPixels();

            var coloredPixels = allPixels.Where(c => c.r > 0 && c.g > 0 && c.b > 0).ToList();

            foreach (var pixel in coloredPixels) {
                spriteMean.x += pixel.r;
                spriteMean.y += pixel.g;
                spriteMean.z += pixel.b;
            }

            Color color = spriteMean / coloredPixels.Count;

            color.a = 1;
            GetComponentInChildren<Light2D>().color = color;
        }
    }
}