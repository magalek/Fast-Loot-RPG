using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] Player player;
    [SerializeField] [Range(0.1f, 2f)] float turnTime = 1f;
    [SerializeField] Enemy[] enemyPrefabs;
    [SerializeField] Enemy[] bossPrefabs;

    public static GameManager Instance;

    public int killCount;
    public List<Battle> battles;

    bool instantBattle;
    bool battleWon = true;
    public static Enemy enemy;

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

    private void Update()
    {
        if (!battleWon)
            BattleLog.Instance.SendMessageToBattleLog("You lost");
    }

    public void LoadLocation() => SceneManager.LoadScene(1);
    public void LoadMenu() => SceneManager.LoadScene(0);

    private void OnSceneLoaded(Scene scene, LoadSceneMode arg1)
    {
        if (scene.buildIndex == 1)
            StartBattle();
        if (scene.buildIndex == 0)
            Player.Instance.statistics.healthPoints = Player.Instance.statistics.maxHealthPoints;
    }

    private void StartBattle()
    {
        //Battle battle = new Battle();
        enemy = GetEnemy();
        //Battle.Actual.enemy = enemy;

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
            {
                player.abilityManager.RefreshCooldowns(max: true);
                break;
            }

            HandleTurn(player, enemy);
            EnemyEventHandler.OnEnemyHit(enemy);

            if (!instantBattle)
                yield return new WaitForSeconds(turnTime);

            enemy.abilityManager.RefreshCooldowns(amount: 1);
            player.abilityManager.RefreshCooldowns(amount: 1);
        }

        if (enemy.statistics.healthPoints <= 0)
        {
            Item item = enemy.DropItem(enemy);

            if (item != null)
                Inventory.Instance.AddItem(item);

            HandleLootUIText(item);

            InventoryEventHandler.OnInventoryChange();

            enemy.Kill();
            killCount++;

            if (!instantBattle)
                yield return new WaitForSeconds(turnTime);

            StartBattle();
        }
        else
        {
            enemy.Kill();
            LoadMenu();
        }
    }

    private void HandleTurn(Entity attacker, Entity target)
    {
        AttackInfo attackInfo = attacker.abilityManager.GetAbility().Invoke(attacker, target);

        BattleEventHandler.OnActionDone(attacker, attackInfo);

        //Battle.Actual.turns.Add(new Turn(attacker, target, attackInfo));
    }

    private void HandleLootUIText(Item item)
    {
        if (item != null)
            BattleLog.Instance.SendMessageToBattleLog($"You got <color=#{ColorUtility.ToHtmlStringRGB(item.color)}>{item.name}</color>");
        else
            BattleLog.Instance.SendMessageToBattleLog("You got nothing");
    }

    private void InitializeEnemyDatabase()
    {
        enemyPrefabs = Resources.LoadAll<Enemy>("Prefabs/Enemy Prefabs/Normal");
        bossPrefabs = Resources.LoadAll<Enemy>("Prefabs/Enemy Prefabs/Bosses");
    }

    public void ChangeTurnTime(float value) => turnTime = value;

    public void ToggleInstantBattle(bool toggled) => instantBattle = toggled;

    Enemy GetEnemy()
    {
        if (killCount >= 100 && UnityEngine.Random.value <= 0.04f)
            return Instantiate(bossPrefabs[UnityEngine.Random.Range(0, bossPrefabs.Length)], transform).GetComponent<Enemy>();
        else
            return Instantiate(enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Length)], transform).GetComponent<Enemy>();
    }

}
