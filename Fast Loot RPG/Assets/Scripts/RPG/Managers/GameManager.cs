using System.Collections;
using System.Collections.Generic;
using RPG.Entities;
using RPG.Items;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] [Range(0.1f, 2f)] private float turnTime = 1f;
    
        public static GameManager Instance = null;

        public int killCount;

        private bool InstantBattle;
        private static Enemy enemy;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);
            

            DontDestroyOnLoad(gameObject);
            ResourceManager.Initialise();

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public void LoadLocation() => SceneManager.LoadScene(1);
        private void LoadMenu() => SceneManager.LoadScene(0);

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
            player = Player.Instance;
            //Battle.Actual.enemy = enemy;

            BattleEventHandler.OnBattleStart(player, enemy);

            StartCoroutine(BattleCoroutine(player, enemy));
        }

        private IEnumerator BattleCoroutine(Player player, Enemy enemy)
        {
            while (player.statistics.healthPoints > 0 && enemy.statistics.healthPoints > 0)
            {
                HandleTurn(enemy, player);
                PlayerEventHandler.OnPlayerHit(player);

                if (!InstantBattle)
                    yield return new WaitForSeconds(turnTime);

                if (player.statistics.healthPoints <= 0)
                {
                    PlayerEventHandler.OnPlayerDeath(player);
                    break;
                }

                HandleTurn(player, enemy);
                EnemyEventHandler.OnEnemyHit(enemy);

                if (!InstantBattle)
                    yield return new WaitForSeconds(turnTime);

                BattleEventHandler.OnTurnEnd();
            }

            if (enemy.statistics.healthPoints <= 0)
            {
                Item item = enemy.DropItem(enemy);

                if (item != null)
                    Inventory.AddItem(item);

                HandleLootUIText(item);

                enemy.Kill();
                killCount++;

                if (!InstantBattle)
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
                BattleLog.SendMessageToBattleLog($"You got <color=#{ColorUtility.ToHtmlStringRGB(item.color)}>{item.name}</color>");
            else
                BattleLog.SendMessageToBattleLog("You got nothing");
        }

        public void ChangeTurnTime(float value) => turnTime = value;

        public void ToggleInstantBattle(bool toggled) => InstantBattle = toggled;

        private Enemy GetEnemy()
        {
            if (killCount >= 100 && Random.value <= 0.04f)
                return Instantiate(ResourceManager.bossPrefabs[Random.Range(0, ResourceManager.bossPrefabs.Length)], transform).GetComponent<Enemy>();
            return Instantiate(ResourceManager.enemyPrefabs[Random.Range(0, ResourceManager.enemyPrefabs.Length)], transform).GetComponent<Enemy>();
        }

    }
}
