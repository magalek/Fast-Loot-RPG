using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {

    [SerializeField] TextMeshProUGUI battleLogText;
    [SerializeField] Player player;   
    [SerializeField] GameObject enemyPrefab;

    bool battleWon;
    Enemy enemy;

    private void Start()
    {
        battleWon = true;
        Battle();
    }

    private void Update()
    {
        if (!battleWon)
            battleLogText.text = "You lost";

    }

    private void Battle()
    {
        enemy = Instantiate(enemyPrefab,transform).GetComponent<Enemy>();
        StartCoroutine(BattleCoroutine(player, enemy));
    }

    IEnumerator BattleCoroutine(Player player, Enemy enemy)
    {
        while (player.healthPoints > 0 && enemy.healthPoints > 0)
        {
            HandleTurn(enemy, player);

            yield return new WaitForSeconds(2f);

            if (player.healthPoints <= 0)
                break;

            HandleTurn(player, enemy);

            yield return new WaitForSeconds(2f);
        }
        if (enemy.healthPoints <= 0)
        {
            Item loot = enemy.DropLoot(enemy);

            HandleLootUIText(loot);

            enemy.Kill();
            yield return new WaitForSeconds(2f);
            Battle();
        }
        else battleWon = false;
        
    }

    private void HandleTurn(Entity attacker, Entity target)
    {
        int attackerDamage = attacker.CalculateDamage(attacker, target);
        target.healthPoints -= attackerDamage;
        if (attacker is Enemy)
            battleLogText.text = attacker.entityName + " hit you for " + attackerDamage;
        else
            battleLogText.text = "You hit " + enemy.entityName + " for " + attackerDamage;
    }

    private void HandleLootUIText(Item loot)
    {
        if (loot != null)
        {
            Inventory.Instance.AddToInventory(loot);
            if (loot.rarity == Rarity.Legendary)
                battleLogText.text = "You got <color=\"orange\">" + loot.name + "</color>";
            else
                battleLogText.text = "You got " + loot.name;
        }
        else
            battleLogText.text = "You got nothing";
    }
}
