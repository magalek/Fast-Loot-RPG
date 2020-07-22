using RPG;
using RPG.Controllers;
using RPG.Generators;
using RPG.UI;
using TMPro;
using UnityEngine;

public class Stairs : MonoBehaviour, IInteractable {

    private TextMeshProUGUI textComponent;

    private void Awake() {
        GetComponentInChildren<Canvas>().worldCamera = MainCamera.Instance.camera;
        textComponent = GetComponentInChildren<TextMeshProUGUI>();
        textComponent.gameObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collider) {
        if (collider.CompareTag("Player")) {
            textComponent.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            textComponent.gameObject.SetActive(false);
        }
    }

    public void Interact(GameObject client) {
        if (Input.GetKey(KeyCode.E) && LevelGenerator.GeneratingLevel == false) {
            LevelGenerator.LevelNumber++;
            GameController.Instance.RestartGame();
        }
    }
}
