using System;
using RPG.Entities;
using RPG.Items;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI {
    public class PlayerUI : MonoBehaviour {

        public event Action CharacterInfoHidden;

        [SerializeField] private Image hpBarImage;

        private GameObject infoGameObject;

        private void Awake() {
            GetComponent<Canvas>().worldCamera = MainCamera.Instance.GetComponent<Camera>();
            Player.Instance.health.Changed += ChangePlayerHealthBar;

            infoGameObject = transform.Find("Character Info").gameObject;
        }

        private void Start() {
            infoGameObject.SetActive(false);
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.E)) {
                infoGameObject.SetActive(!infoGameObject.activeSelf);
                if (infoGameObject.activeSelf == false)
                    CharacterInfoHidden?.Invoke();
            }
        }

        private void ChangePlayerHealthBar()
            => hpBarImage.fillAmount = Player.Instance.health.percentage;

    }
}
