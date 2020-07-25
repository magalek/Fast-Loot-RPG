using System;
using System.Collections.Generic;
using System.Linq;
using RPG.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI {
    public class PlayerUI : MonoBehaviour {

        public event Action CharacterInfoHidden;

        public static PlayerUI Instance;
        
        [SerializeField] private Image hpBarImage;
        public GameObject staticUI;
        
        public GameObject infoGameObject;

        public bool canOpenInventory = true;
        
        private void Awake() {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);

            infoGameObject = transform.Find("Character Info").gameObject;
        }

        private void Start() {
            Player.Instance.characterInfo.Health.Changed += ChangePlayerHealthBar;

            GetComponent<Canvas>().worldCamera = MainCamera.Instance.GetComponent<Camera>();
            GetComponent<Canvas>().sortingLayerID = SortingLayer.NameToID("UI");
            infoGameObject.SetActive(false);
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.I) && canOpenInventory) {
                infoGameObject.SetActive(!infoGameObject.activeSelf);
                Player.Instance.inventory.ChangeState(infoGameObject.activeSelf);
                if (infoGameObject.activeSelf == false)
                    CharacterInfoHidden?.Invoke();
            }
        }

        private void ChangePlayerHealthBar()
            => hpBarImage.fillAmount = Player.Instance.characterInfo.Health.Percentage;

    }
}
