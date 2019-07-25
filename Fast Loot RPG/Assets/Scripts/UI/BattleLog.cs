using TMPro;
using UnityEngine;

public class BattleLog : MonoBehaviour
{

    public static BattleLog Instance;

    [SerializeField] TextMeshProUGUI battleLogTextObject;

    static TextMeshProUGUI battleLogText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        battleLogText = battleLogTextObject;
        PlayerEventHandler.PlayerDeath += () => SendMessageToBattleLog("You lost");
    }

    public static void SendMessageToBattleLog(string message) => battleLogText.text = message;
}
