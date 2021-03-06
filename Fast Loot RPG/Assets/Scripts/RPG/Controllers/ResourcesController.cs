﻿using System;
using System.Collections.Generic;
using System.Linq;
using RPG.Entities;
using RPG.Items;
using UnityEngine;

namespace RPG.Controllers
{
    public static class ResourcesController
    {
        public static List<GameObject> enemyPrefabs;
        public static List<GameObject> npcPrefabs;
        public static Enemy[] bossPrefabs;
        public static List<GameObject> itemObjectsPrefabs;
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
                InitialiseNPCPrefab();
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
            enemyPrefabs = Resources.LoadAll<GameObject>("Prefabs/Enemy Prefabs/Normal").ToList();
            bossPrefabs = Resources.LoadAll<Enemy>("Prefabs/Enemy Prefabs/Bosses");
        }
        
        private static void InitialiseNPCPrefab() {
            npcPrefabs = Resources.LoadAll<GameObject>("Prefabs/NPC").ToList();
        }

        private static void InitialiseGameControllerPrefab()
            => gameControllerPrefab = Resources.Load<GameObject>("Prefabs/Scene Prefabs/Game Manager");
        
        private static void InitialisePlayerPrefab()
            => playerPrefab = Resources.Load<GameObject>("Prefabs/Scene Prefabs/Player");
        
        private static void InitialiseItemDatabase() => itemObjectsPrefabs = Resources.LoadAll<GameObject>("Prefabs/Item Prefabs").ToList();
    }
}