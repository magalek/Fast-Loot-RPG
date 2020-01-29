using System;
using RPG.Entities;
using RPG.Items;
using UnityEngine;

namespace RPG.Managers
{
    public static class ResourceManager
    {
        public static Enemy[] enemyPrefabs;
        public static  Enemy[] bossPrefabs;
        public static Item[] itemPrefabs;
        
        public static bool Initialise()
        {
            try
            {
                InitialiseEnemyResources();
                InitialiseItemDatabase();

            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
    
        private static void InitialiseEnemyResources()
        {
            enemyPrefabs = Resources.LoadAll<Enemy>("Prefabs/Enemy Prefabs/Normal");
            bossPrefabs = Resources.LoadAll<Enemy>("Prefabs/Enemy Prefabs/Bosses");
        }
        
        private static void InitialiseItemDatabase() => itemPrefabs = Resources.LoadAll<Item>("Prefabs/Item Prefabs");
    }
}