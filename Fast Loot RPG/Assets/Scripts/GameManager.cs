using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {

    [SerializeField] TextMeshProUGUI battleLogText;
    [SerializeField] Player player;   
    [SerializeField] [Range(0.1f, 2f)] float turnTime = 1f;
    [SerializeField] Enemy[] enemyPrefabs;


    bool battleWon;
    Enemy enemy;

    private void Awake()
    {
        enemyPrefabs = Resources.LoadAll<Enemy>("Prefabs/Enemy Prefabs");
    }

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
        enemy = Instantiate(enemyPrefabs[UnityEngine.Random.Range(0,enemyPrefabs.Length)],transform).GetComponent<Enemy>();
        StartCoroutine(BattleCoroutine(player, enemy, turnTime));
    }

    IEnumerator BattleCoroutine(Player player, Enemy enemy, float turnTime)
    {
        while (player.healthPoints > 0 && enemy.healthPoints > 0)
        {
            HandleTurn(enemy, player);

            yield return new WaitForSeconds(turnTime);

            if (player.healthPoints <= 0)
                break;

            HandleTurn(player, enemy);

            yield return new WaitForSeconds(turnTime);
        }
        if (enemy.healthPoints <= 0)
        {
            Item loot = enemy.DropLoot(enemy);

            HandleLootUIText(loot);

            enemy.Kill();
            yield return new WaitForSeconds(turnTime);
            Battle();
        }
        else battleWon = false;
        
    }

    private void HandleTurn(Entity attacker, Entity target)
    {
        int attackerDamage = attacker.CalculateDamage(attacker, target);
        target.healthPoints -= attackerDamage;
        battleLogText.text = attacker.entityName + " hit " + target.entityName + " for " + attackerDamage;

    }

    private void HandleLootUIText(Item loot)
    {
        if (loot != null)
        {
            Inventory.Instance.AddToInventory(loot);
            if (loot.rarity == ItemRarity.Legendary)
                battleLogText.text = "You got <color=\"orange\">" + loot.name + "</color>";
            else
                battleLogText.text = "You got " + loot.name;
        }
        else
            battleLogText.text = "You got nothing";

        if (loot != null) Debug.Log(loot.itemLevel);
    }
}
