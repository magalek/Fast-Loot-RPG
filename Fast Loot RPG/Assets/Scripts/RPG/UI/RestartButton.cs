using RPG.Controllers;
using RPG.Generators;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI {
    public class RestartButton : MonoBehaviour
    {
        private void Start() {
            GetComponent<Button>().onClick.AddListener(GameController.Instance.RestartGame);
        }
    }
}
