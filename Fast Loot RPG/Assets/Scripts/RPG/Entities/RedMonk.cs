using System;
using RPG.Entities.Movement;
using RPG.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Entities {
    public class RedMonk : NPC, IInteractable {

        private event Action DamageBought;
        private event Action HealthBought;
        
        private GameObject damageSlot;
        private GameObject healthSlot;

        [SerializeField] private int damageCost = 100;
        [SerializeField] private int healthCost = 60;
        
        public override void Awake() {
            base.Awake();

            damageSlot = GameObject.Find("Damage Slot");
            damageSlot.GetComponentInChildren<Button>().onClick.AddListener(AddDamage);
            healthSlot = GameObject.Find("Health Slot");
            healthSlot.GetComponentInChildren<Button>().onClick.AddListener(AddHealth);

            damageCost = 100;
            healthCost = 60;
            
            ChangeTexts();
            
            DamageBought += () => {
                damageCost = (int) (damageCost * 1.8);
                ChangeTexts();
            };
            HealthBought += () => {
                healthCost = (int) (healthCost * 1.8);
                ChangeTexts();
            };

            Player.Died += () => {
                damageCost = 100;
                healthCost = 60;
            };
        }

        private void ChangeTexts() {
            damageSlot.GetComponentInChildren<TextMeshProUGUI>().text = $"Increase <b>damage</b> for {damageCost}";
            healthSlot.GetComponentInChildren<TextMeshProUGUI>().text = $"Increase <b>health</b> for {healthCost}";
        }
        
        private void AddHealth() {
            if (Score.Instance.Amount < healthCost) return;

            Score.Instance.Amount -= healthCost;
            Player.Instance.characterInfo.Health.Max += 20;
            Player.Instance.characterInfo.Health.Current += 20;
            HealthBought?.Invoke();
        }
        
        private void AddDamage() {
            if (Score.Instance.Amount < damageCost) return;

            Score.Instance.Amount -= damageCost;
            Player.Instance.characterInfo.Damage.Current += 5;
            DamageBought?.Invoke();
        }
        
        public void Interact(GameObject client) {
            IsInteracting = !IsInteracting;
            if (IsInteracting) {
                if (client.transform.position.x < transform.position.x) {
                    GetComponent<SpriteRenderer>().flipX = true;
                }
                else {
                    GetComponent<SpriteRenderer>().flipX = false;
                }

                //GetComponent<NPCContainer>().ChangeState(true);

                NPCUI.SetActive(true);

                PlayerUI.Instance.staticUI.SetActive(false);
                PlayerUI.Instance.canOpenInventory = false;
                // PlayerUI.Instance.infoGameObject.SetActive(true);

                client.GetComponent<PlayerController>().CanMove = false;
                client.GetComponent<Player>().weapon.CanAttack = false;
                MainCamera.Instance.CenterOn(transform, 0.1f, 0.5f);
            }
            else {

                //GetComponent<NPCContainer>().ChangeState(false);

                NPCUI.SetActive(false);
                PlayerUI.Instance.staticUI.SetActive(true);
                PlayerUI.Instance.canOpenInventory = true;

                // PlayerUI.Instance.infoGameObject.SetActive(false);

                GetComponent<SpriteRenderer>().flipX = false;
                client.GetComponent<PlayerController>().CanMove = true;
                client.GetComponent<Player>().weapon.CanAttack = true;
                MainCamera.Instance.CenterOn(client.transform, 0.1f, 1f);
            }
        }
    }
}