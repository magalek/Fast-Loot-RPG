using RPG.Controllers;
using RPG.UI.Animations;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI {
    public class StartButton : MonoBehaviour {
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(FindObjectOfType<MenuAnimationController>().PlayStartGame);
        }
    }
}
