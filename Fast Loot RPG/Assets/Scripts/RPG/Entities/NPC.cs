using System.Linq;
using RPG.Entities.Movement;
using RPG.UI;
using TMPro;
using UnityEngine;

namespace RPG.Entities {
    public class NPC : Character, IInteractable {

        [SerializeField] private GameObject NPCUI;
        
        private TextMeshProUGUI textComponent;

        private bool isInteracting;
        
        private void Start() {
            GetComponentInChildren<Canvas>().worldCamera = MainCamera.Instance.camera;
            textComponent = GetComponentInChildren<TextMeshProUGUI>();
            textComponent.gameObject.SetActive(false);
            isInteracting = false;
            NPCUI.SetActive(false);
        }

        private void OnTriggerStay2D(Collider2D collider) {
            if (collider.CompareTag("Player") && !isInteracting) {
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
        public void Interact(GameObject client) {
            if (Input.GetKeyDown(KeyCode.E)) {
                isInteracting = !isInteracting;
                if (isInteracting) {
                    if (client.transform.position.x < transform.position.x) {
                        GetComponent<SpriteRenderer>().flipX = true;
                    }
                    else {
                        GetComponent<SpriteRenderer>().flipX = false;
                    }
                    
                    GetComponent<NPCContainer>().ChangeState(true);
                    
                    NPCUI.SetActive(true);
                    
                    PlayerUI.Instance.staticUI.SetActive(false);
                    
                    PlayerUI.Instance.infoGameObject.SetActive(true);
                    Player.Instance.inventory.ChangeState(true);

                    client.GetComponent<PlayerController>().CanMove = false;
                    client.GetComponent<Player>().weapon.CanAttack = false;
                    MainCamera.Instance.CenterOn(transform, 0.1f, 0.5f);
                }
                else {
                    
                    GetComponent<NPCContainer>().ChangeState(false);
                    
                    NPCUI.SetActive(false);
                    PlayerUI.Instance.staticUI.SetActive(true);
                    
                    PlayerUI.Instance.infoGameObject.SetActive(false);
                    Player.Instance.inventory.ChangeState(false);
                    GetComponent<SpriteRenderer>().flipX = false;
                    client.GetComponent<PlayerController>().CanMove = true;
                    client.GetComponent<Player>().weapon.CanAttack = true;
                    MainCamera.Instance.CenterOn(client.transform, 0.1f, 1f);
                }
            }
        }
    }
}