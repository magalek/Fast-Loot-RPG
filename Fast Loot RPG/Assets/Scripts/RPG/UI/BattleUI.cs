using RPG.Events;
using TMPro;
using UnityEngine;

namespace RPG.UI {
    public class BattleUI : MonoBehaviour {

        [SerializeField] private TextMeshProUGUI battleLogTextObject;

        private static TextMeshProUGUI battleLogText;

        private void Awake()
        {
            battleLogText = battleLogTextObject;
            PlayerEvents.PlayerDeath += () => DisplayMessage("You lost");
        }
        public static void DisplayMessage(string message) => battleLogText.text = message;
    }
}
