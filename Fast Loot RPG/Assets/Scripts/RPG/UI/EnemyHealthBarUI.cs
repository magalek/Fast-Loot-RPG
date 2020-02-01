using RPG.Entities;
using RPG.Events;
using TMPro;
using UnityEngine;

namespace RPG.UI {
    public class EnemyHealthBarUI : MonoBehaviour
    {
        private TextMeshProUGUI hpText;

        private void Awake()
        {
            hpText = GetComponent<TextMeshProUGUI>();
            BattleEvents.BattleStart += ChangeHealthText;
            EnemyEvents.EnemyHit += ChangeHealthText;
        }

        private void ChangeHealthText(Enemy enemy) 
            => hpText.text = $"{enemy.statistics.maxHealthPoints} / {enemy.statistics.healthPoints}";

        private void ChangeHealthText(Player player, Enemy enemy) 
            => hpText.text = $"{enemy.statistics.maxHealthPoints} / {enemy.statistics.healthPoints}";
    }
}
