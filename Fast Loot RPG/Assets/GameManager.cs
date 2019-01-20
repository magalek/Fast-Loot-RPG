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
            int enemyDamage = enemy.CalculateDamage(enemy, player);
            player.healthPoints -= enemyDamage;
            battleLogText.text = enemy.entityName + " hit you for " + enemyDamage;
            yield return new WaitForSeconds(2f);
            if (player.healthPoints <= 0)
                break;
            int playerDamage = player.CalculateDamage(player, enemy);
            enemy.healthPoints -= playerDamage;
            battleLogText.text = "You hit " + enemy.entityName + " for " + playerDamage;
            yield return new WaitForSeconds(2f);
        }
        if (enemy.healthPoints <= 0)
        {
            Item loot = enemy.DropLoot(enemy);
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
            enemy.Kill();
            yield return new WaitForSeconds(2f);
            Battle();
        }
        else battleWon = false;
        
    }
}
