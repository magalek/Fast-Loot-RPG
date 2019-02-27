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
    [SerializeField] Enemy[] bossPrefabs;

    public static GameManager Instance;

    public int killCount;

    bool instantBattle;
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
        //Debug.Log("awake");
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

            if (!instantBattle)
                yield return new WaitForSeconds(turnTime);

            if (player.statistics.healthPoints <= 0)
                break;

            HandleTurn(player, enemy);
            EnemyEventHandler.OnEnemyHit(enemy);

            if (!instantBattle)
                yield return new WaitForSeconds(turnTime);

            enemy.abilityManager.RefreshCooldowns();
            player.abilityManager.RefreshCooldowns();
        }
        if (enemy.statistics.healthPoints <= 0)
        {
            Item item = enemy.DropItem(enemy);

            if (item != null)
                Inventory.Instance.AddItem(item);

            HandleLootUIText(item);

            // DO POPRAWY BO CALY CZAS GDZIES NULL JEST
            Inventory.Instance.SortItems();

            enemy.Kill();
            killCount++;

            if (!instantBattle)
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
            BattleLog.Instance.SendMessageToBattleLog($"You got <color=\"{(item.rarity == ItemRarity.Common ? "green" : "yellow") }\">{item.name}</color>");
        }
        else
            BattleLog.Instance.SendMessageToBattleLog("You got nothing");

        if (item != null) ; //Debug.Log(item.itemLevel);
    }

    private void InitializeEnemyDatabase()
    {
        enemyPrefabs = Resources.LoadAll<Enemy>("Prefabs/Enemy Prefabs/Normal");
        bossPrefabs = Resources.LoadAll<Enemy>("Prefabs/Enemy Prefabs/Bosses");
    }

    public void ChangeTurnTime(float value)
    {
        turnTime = value;
    }

    public void ToggleInstantBattle(bool toggled)
    {
        instantBattle = toggled;
    }

    Enemy HandleEnemySpawn()
    {
        if (killCount >= 100 && UnityEngine.Random.value <= 0.04f)
            return Instantiate(bossPrefabs[UnityEngine.Random.Range(0, bossPrefabs.Length)], transform).GetComponent<Enemy>();
        else
            return Instantiate(enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Length)], transform).GetComponent<Enemy>();
    }

}
