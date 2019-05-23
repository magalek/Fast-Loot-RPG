using TMPro;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{

    TextMeshProUGUI hpText;

    private void Awake()
    {
        hpText = GetComponent<TextMeshProUGUI>();
        PlayerEventHandler.PlayerHit += ChangeHealthText;
        BattleEventHandler.BattleStart += ChangeHealthText;
        InventoryEventHandler.InventoryChange += ChangeHealthText;
    }

    public void ChangeHealthText() => hpText.text = $"{Player.Instance.statistics.maxHealthPoints} / {Player.Instance.statistics.healthPoints}";

    public void ChangeHealthText(Player player) => hpText.text = $"{player.statistics.maxHealthPoints} / {player.statistics.healthPoints}";

    public void ChangeHealthText(Player player, Enemy enemy) => hpText.text = $"{player.statistics.maxHealthPoints} / {player.statistics.healthPoints}";

    public void ChangeHealthText(Item item) => hpText.text = $"{Player.Instance.statistics.maxHealthPoints} / {Player.Instance.statistics.healthPoints}";

}
