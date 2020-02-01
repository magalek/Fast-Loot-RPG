using RPG.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI {
    public class PlayerUI : MonoBehaviour {

        [SerializeField] private Canvas playerUICanvas;
        [SerializeField] private Image playerHealthBarImage;

        private void Awake() {
            playerUICanvas.worldCamera = MainCamera.Instance.GetComponent<Camera>();
            Player.Instance.HealthChanged += ChangePlayerHealthBar;
        }

        private void ChangePlayerHealthBar()
            => playerHealthBarImage.fillAmount = Player.Instance.healthPercentage;

    }
}
