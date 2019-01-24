using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField] TextMeshProUGUI battleLogText;
    [SerializeField] Player player;   
    [SerializeField] [Range(0.1f, 2f)] float turnTime = 1f;
    [SerializeField] Enemy[] enemyPrefabs;


    bool battleWon;
    Enemy enemy;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        InitializeEnemyDatabase();
    }

    private void Start()
    {
        battleWon = true;        
        //Battle();
    }

    private void Update()
    {
        if (!battleWon)
            battleLogText.text = "You lost";
    }

    public void LoadLocation()
    {
        SceneManager.LoadScene(1);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode arg1)
    {
        Battle();
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
            Item item = enemy.DropItem(enemy);

            HandleLootUIText(item);

            enemy.Kill();
            yield return new WaitForSeconds(turnTime);
            Battle();
        }
        else
        {
            LoadMenu();
            StopCoroutine("BattleCoroutine");
        }
    }

    private void HandleTurn(Entity attacker, Entity target)
    {
        int attackerDamage = attacker.CalculateDamage(attacker, target);
        target.healthPoints -= attackerDamage;
        battleLogText.text = attacker.entityName + " hit " + target.entityName + " for " + attackerDamage;

    }

    private void HandleLootUIText(Item item)
    {
        if (item != null)
        {
            Inventory.Instance.AddToInventory(item);
            //if (item.rarity == ItemRarity.Legendary)
            //    battleLogText.text = $"You got <color=\"{(item.rarity == ItemRarity.Common ? "white" : "orange") }\"> + {item.name} + </color>";
            //else
            //    battleLogText.text = "You got " + item.name;
            battleLogText.text = $"You got <color=\"{(item.rarity == ItemRarity.Common ? "green" : "yellow") }\">{item.name}</color>";
        }
        else
            battleLogText.text = "You got nothing";

        if (item != null) Debug.Log(item.itemLevel);
    }

    private void InitializeEnemyDatabase()
    {
        enemyPrefabs = Resources.LoadAll<Enemy>("Prefabs/Enemy Prefabs");
    }
}
