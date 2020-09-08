using RPG.Entities;
using TMPro;
using UnityEngine;

namespace RPG {
    public class Currency : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI textComponent;
        public int Amount {
            get => amount;
            set {
                amount = value;
                textComponent.text = Amount.ToString();
            } 
        }

        public static Currency Instance;

        private int amount;
    
        private void Awake() {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);

            Player.Died += () => Amount = 0;
        }

        private void Start() {
            textComponent = GetComponent<TextMeshProUGUI>();
        }
    }
}