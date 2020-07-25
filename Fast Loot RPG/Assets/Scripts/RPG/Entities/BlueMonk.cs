using RPG.Entities.Movement;
using RPG.UI;
using UnityEngine;

namespace RPG.Entities {
    public class BlueMonk : NPC, IInteractable {
        public void Interact(GameObject client) {
            IsInteracting = !IsInteracting;
            if (IsInteracting) {
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