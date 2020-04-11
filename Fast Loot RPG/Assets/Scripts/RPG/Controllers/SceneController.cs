using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Controllers {
    public static class SceneController {
        
        public static void LoadGame() {
            SceneManager.LoadScene("GameScene");
        }

        public static void ExitGame() {
            Application.Quit();
        }
    }
}