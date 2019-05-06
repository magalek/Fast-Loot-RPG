using TMPro;
using UnityEngine;

public class BattleLog : MonoBehaviour
{

    public static BattleLog Instance;

    [SerializeField] TextMeshProUGUI battleLogText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void SendMessageToBattleLog(string message)
    {
        battleLogText.text = message;
    }
}
