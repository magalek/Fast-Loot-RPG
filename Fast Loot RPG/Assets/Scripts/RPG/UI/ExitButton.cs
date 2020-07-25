using RPG.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI {
    public class ExitButton : MonoBehaviour
    {
        private void Start() {
            GetComponent<Button>().onClick.AddListener(() => {
                SceneController.ExitGame();
                Debug.Log("exit");
            });
        }
    }
}
