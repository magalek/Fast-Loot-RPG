﻿using System.Collections;
using RPG.Entities;
using RPG.Events;
using RPG.Items;
using RPG.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Controllers
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] [Range(0.1f, 2f)] private float turnTime = 1f;
    
        public static GameController Instance = null;

        public int killCount;

        private bool InstantBattle;
        private static Enemy currentEnemy;

        private void Awake() {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);

            StartLevelGenerating(20, 1.5f);
            
            DontDestroyOnLoad(gameObject);
        }

        public void StartLevelGenerating(int roomAmount, float distance) {
            StartCoroutine(LevelController.GenerateLevel(roomAmount, distance));
        }

        private Enemy GetEnemy() {
            if (killCount >= 100 && Random.value <= 0.04f)
                return Instantiate(ResourcesController.bossPrefabs[Random.Range(0, ResourcesController.bossPrefabs.Length)], transform).GetComponent<Enemy>();
            return Instantiate(ResourcesController.enemyPrefabs[Random.Range(0, ResourcesController.enemyPrefabs.Length)], transform).GetComponent<Enemy>();
        }

    }
}
