using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField] Player player;   
    [SerializeField] [Range(0.1f, 2f)] float turnTime = 1f;
    [SerializeField] Enemy[] enemyPrefabs;

    public static GameManager Instance;

    public int killCount;

    bool battleWon;
    Enemy enemy;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(this);
        InitializeEnemyDatabase();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        battleWon = true;
    }

    private void Update()
    {
        if (!battleWon)
            BattleLog.Instance.SendMessageToBattleLog("You lost");
    }

    public void LoadLocation()
    {
        SceneManager.LoadScene(1);        
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode arg1)
    {
        if (scene.buildIndex == 1)
            Battle();
        if (scene.buildIndex == 0)
            Player.Instance.statistics.healthPoints = Player.Instance.statistics.maxHealthPoints;
    }

    private void Battle()
    {
        enemy = HandleEnemySpawn();
        BattleEventHandler.OnBattleStart(player, enemy);
        StartCoroutine(BattleCoroutine(player, enemy));     
    }

    IEnumerator BattleCoroutine(Player player, Enemy enemy)
    {
        while (player.statistics.healthPoints > 0 && enemy.statistics.healthPoints > 0)
        {            
            HandleTurn(enemy, player);
            PlayerEventHandler.OnPlayerHit(player);

            yield return new WaitForSeconds(turnTime);

            if (player.statistics.healthPoints <= 0)
                break;

            HandleTurn(player, enemy);
            EnemyEventHandler.OnEnemyHit(enemy);

            yield return new WaitForSeconds(turnTime);
            enemy.abilityManager.RefreshCooldowns();
            player.abilityManager.RefreshCooldowns();
        }
        if (enemy.statistics.healthPoints <= 0)
        {
            Item item = enemy.DropItem(enemy);

            HandleLootUIText(item);

            enemy.Kill();
            yield return new WaitForSeconds(turnTime);
            Battle();
        }
        else
        {
            enemy.Kill();
            LoadMenu();            
        }
    }

    private void HandleTurn(Entity attacker, Entity target)
    {
        attacker.abilityManager.GetAbility().Execute(attacker, target);
    }

    private void HandleLootUIText(Item item)
    {
        if (item != null)
        {
            Inventory.Instance.AddToInventory(item);
            BattleLog.Instance.SendMessageToBattleLog($"You got <color=\"{(item.rarity == ItemRarity.Common ? "green" : "yellow") }\">{item.name}</color>");
        }
        else
            BattleLog.Instance.SendMessageToBattleLog("You got nothing");

        if (item != null) Debug.Log(item.itemLevel);
    }

    private void InitializeEnemyDatabase()
    {
        enemyPrefabs = Resources.LoadAll<Enemy>("Prefabs/Enemy Prefabs");
    }

    public void ChangeTurnTime(float value)
    {
        turnTime = value;
    }

    Enemy HandleEnemySpawn()
    {
        return Instantiate(enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Length)], transform).GetComponent<Enemy>();
    }

}
