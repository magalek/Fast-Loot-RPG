using TMPro;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{

    TextMeshProUGUI hpText;

    private void Awake()
    {
        hpText = GetComponent<TextMeshProUGUI>();
        BattleEventHandler.BattleStart += ChangeHealthText;
        EnemyEventHandler.EnemyHit += ChangeHealthText;
    }

    public void ChangeHealthText(Enemy enemy)
    {
        hpText.text = $"{enemy.statistics.maxHealthPoints} / {enemy.statistics.healthPoints}";
    }

    public void ChangeHealthText(Player player, Enemy enemy)
    {
        hpText.text = $"{enemy.statistics.maxHealthPoints} / {enemy.statistics.healthPoints}";
    }


}
