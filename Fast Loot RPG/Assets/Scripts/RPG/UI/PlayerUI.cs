using RPG.Entities;
using RPG.Items;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI {
    public class PlayerUI : MonoBehaviour {

        [SerializeField] private Canvas playerUICanvas;
        [SerializeField] private Image playerHealthBarImage;

        private GameObject inventoryGameObject;
        private GameObject equipmentGameObject;
        
        private void Awake() {
            inventoryGameObject = transform.parent.GetComponentInChildren<Inventory>().gameObject;
            equipmentGameObject = transform.parent.GetComponentInChildren<Equipment>().gameObject;

            playerUICanvas.worldCamera = MainCamera.Instance.GetComponent<Camera>();
            Player.Instance.health.Changed += ChangePlayerHealthBar;
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.E)) {
                inventoryGameObject.SetActive(!inventoryGameObject.activeSelf);
                equipmentGameObject.SetActive(!equipmentGameObject.activeSelf);
            }
        }

        private void ChangePlayerHealthBar()
            => playerHealthBarImage.fillAmount = Player.Instance.health.percentage;

    }
}
