using System;
using RPG.Entities;
using RPG.Items;
using UnityEngine;

namespace RPG.Controllers
{
    public static class ResourcesController
    {
        public static Enemy[] enemyPrefabs;
        public static Enemy[] bossPrefabs;
        public static GameObject[] itemObjectsPrefabs;
        public static GameObject gameControllerPrefab;
        public static GameObject playerPrefab;
        
        public static bool Initialise()
        {
            try
            {
                InitialiseEnemyPrefabs();
                InitialiseItemDatabase();
                InitialiseGameControllerPrefab();
                InitialisePlayerPrefab();

            }
            catch (Exception e)
            {
                Debug.Log(e);
                return false;
            }
            return true;
        }
    
        private static void InitialiseEnemyPrefabs()
        {
            enemyPrefabs = Resources.LoadAll<Enemy>("Prefabs/Enemy Prefabs/Normal");
            bossPrefabs = Resources.LoadAll<Enemy>("Prefabs/Enemy Prefabs/Bosses");
        }

        private static void InitialiseGameControllerPrefab()
            => gameControllerPrefab = Resources.Load<GameObject>("Prefabs/Scene Prefabs/Game Manager");
        
        private static void InitialisePlayerPrefab()
            => playerPrefab = Resources.Load<GameObject>("Prefabs/Scene Prefabs/Player");
        
        private static void InitialiseItemDatabase() => itemObjectsPrefabs = Resources.LoadAll<GameObject>("Prefabs/Item Prefabs");
    }
}