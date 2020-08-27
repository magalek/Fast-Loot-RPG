using System.Linq;
using RPG.Entities.Movement;
using RPG.UI;
using TMPro;
using UnityEngine;

namespace RPG.Entities {
    public class NPC : Character {

        [SerializeField] internal GameObject NPCUI;
        
        private TextMeshProUGUI textComponent;

        internal bool IsInteracting;
        
        private void Start() {
            var canvases = GetComponentsInChildren<Canvas>();
            foreach (var canvas in canvases) {
                canvas.worldCamera = MainCamera.Instance.camera;
                canvas.sortingLayerName = "UI";
            }
            textComponent = GetComponentInChildren<TextMeshProUGUI>();
            textComponent.gameObject.SetActive(false);
            IsInteracting = false;
            NPCUI.SetActive(false);
        }

        private void OnTriggerStay2D(Collider2D collider) {
            if (collider.CompareTag("Player") && !IsInteracting) {
                textComponent.gameObject.SetActive(true);
            }
            else {
                textComponent.gameObject.SetActive(false);
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                textComponent.gameObject.SetActive(false);
            }
        }
    }
}