using System;
using System.Collections;
using RPG.Entities;
using RPG.Events;
using RPG.Generators;
using RPG.Items;
using RPG.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Controllers
{
    public class GameController : MonoBehaviour {
        public static event Action GameRestarted;
        [SerializeField] private Player player;
        [SerializeField] [Range(0.1f, 2f)] private float turnTime = 1f;
    
        public static GameController Instance = null;
        
        public int killCount;

        private void Awake() {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);

            
            if (!LevelGenerator.Initialised) 
                LevelGenerator.Init();

            DontDestroyOnLoad(gameObject);
        }

        private void Start() {
            LevelGenerator.GenerateLevel();
        }

        public void RestartGame() {
            LevelGenerator.ClearLevel();
            LevelGenerator.GenerateLevel();
            GameRestarted?.Invoke();
        }
    }
}
