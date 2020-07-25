using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RPG.Controllers;
using RPG.Entities;
using RPG.Environment;
using RPG.Materials;
using RPG.Utility;
using RPG.Statistics;
using UnityEngine;
using CharacterInfo = RPG.Statistics.CharacterInfo;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace RPG.Generators {
    public static class LevelGenerator {
        public static event Action GenerationCompleted;
        
        public static event Action LevelRestarted;

        public static bool Initialised = false;

        public static int DefaultRoomAmount = 20;

        public static int LevelNumber = 1;

        public static bool GeneratingLevel = false;
        private static GameObject horizontalCorridorPrefab => Resources.Load<GameObject>("Prefabs/Environment Prefabs/Corridor Horizontal");
        private static GameObject verticalCorridorPrefab => Resources.Load<GameObject>("Prefabs/Environment Prefabs/Corridor Vertical");
        private static GameObject stairsPrefab => Resources.Load<GameObject>("Prefabs/Environment Prefabs/Stairs");
        
        private static List<Vector2> roomPositions = new List<Vector2>();

        private static float roomOffset = 2;

        private static Transform levelParent;
        
        private static Transform environmentParent;

        private static Transform enemiesParent;

        private static Transform npcParent;
        
        private static List<Room> rooms = new List<Room>();
        
        
        public static void Init() {
            RoomGenerator.Init();
            Initialised = true;
            GenerationCompleted += () => GeneratingLevel = false;
            Player.Died += () => LevelNumber = 1;
        }
        
        public static void GenerateLevel(int roomAmount = 0) {
            GeneratingLevel = true;
            ItemsController.Instance.ClearItems();
            
            if (roomAmount == 0) roomAmount = DefaultRoomAmount; 
            
            levelParent = new GameObject("Level").transform;
            
            environmentParent = new GameObject("Environment").transform;
            environmentParent.SetParent(levelParent);
            
            enemiesParent = new GameObject("Enemies").transform;
            enemiesParent.SetParent(levelParent);
            
            npcParent = new GameObject("NPCs").transform;
            npcParent.SetParent(levelParent);
            
            GenerateRooms(roomAmount);

            GenerateCorridors();

            GenerateEntities(0, 8, 0.8f);

            GenerateStairs();

            Bounds bounds = new Bounds(roomPositions[0], Vector3.zero);
            bounds.Encapsulate(roomPositions.Last());
            Debug.Log(bounds.center);
            
            GenerationCompleted?.Invoke();
        }

        private static void GenerateStairs() {
            SpawnPoint spawnPoint = rooms[rooms.Count - 1].SpawnPoints.Random();
            GameObject stairs = Object.Instantiate(stairsPrefab, spawnPoint.transform.position, Quaternion.identity);
            stairs.transform.SetParent(levelParent);
        }

        private static void GenerateCorridors() {
            int index = 0;
            foreach (var position in RoomPosition.positions) {
                var corridor = CreateCorridor(position);
                corridor.name = $"Corridor {index}";
                corridor.transform.SetParent(environmentParent);
                index++;
            }
        }

        private static void GenerateRooms(int amount) {
            RoomPosition nextPosition = RoomPosition.Zero();

            for (int i = 0; i < amount - 1; i++) {
                var currentPosition = nextPosition;
                if (i < amount - 2)
                    nextPosition = GetNextRoomPosition(currentPosition);

                var room = RoomGenerator.CreateRoom(currentPosition, nextPosition);
                rooms.Add(room.GetComponent<Room>());
                room.name = $"Room {i}";
                room.transform.SetParent(environmentParent);
                roomPositions.Add(currentPosition.vector2);

                if (i < amount - 2)
                    RoomPosition.positions.Add(nextPosition);
            }
            
            rooms.RemoveAt(0);
        }
        
        private static void GenerateEntities(int minAmount, int maxAmount, float npcChance) {
            int npcRoom = Random.value < npcChance ? Random.Range(0, rooms.Count) : -1; 
            
            for (int i = 1; i < rooms.Count; i++) {
                if (i == npcRoom) {
                    SpawnPoint spawnPoint = rooms[i].SpawnPoints.Random();
                    GameObject npcPrefab = ResourcesController.npcPrefabs.Random();
                    GameObject npc = Object.Instantiate(npcPrefab, spawnPoint.transform.position, Quaternion.identity);
                    npc.transform.SetParent(levelParent);
                }
                else {
                    int count = Random.Range(minAmount, maxAmount);
                            
                    List<SpawnPoint> spawnPoints = rooms[i].SpawnPoints;
    
                    for (int j = 0; j < count; j++) {
                        SpawnPoint spawnPoint = spawnPoints.Random();
                        spawnPoints.Remove(spawnPoint);
                        GameObject enemyPrefab = ResourcesController.enemyPrefabs.Random();
                        GameObject enemy =  Object.Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity);
                        RandomizeEnemyStats(ref enemy.GetComponent<Enemy>().characterInfo, out float modifier);
                        enemy.transform.localScale *= modifier;
                        enemy.transform.SetParent(enemiesParent);
                    }
                }
            }
        }
        
        public static void ClearLevel() {
            Object.Destroy(levelParent.gameObject);
            roomPositions.Clear();
            RoomPosition.positions.Clear();
            rooms.Clear();
        }

        private static void RandomizeEnemyStats(ref CharacterInfo info, out float modifier) {
            modifier = Random.Range(0.9f, 1.2f);
            float value = Mathf.Pow(Mathf.Sqrt(Mathf.Pow(LevelNumber, LevelNumber)), 1/15f) * modifier;

            info.Health.Max = (int)(info.Health.Max * value);
            info.Damage.Current = (int)(info.Damage.Current * value);
            info.Init();
        }
        
        private static GameObject CreateCorridor(RoomPosition roomPosition) {
            Vector2 corridorPosition;

            float corridorOffset = roomOffset / 2;
            
            switch (roomPosition.direction) {
                case Direction.Top: corridorPosition = new Vector2(roomPosition.vector2.x, roomPosition.vector2.y - corridorOffset);
                    break;
                case Direction.Right: corridorPosition = new Vector2(roomPosition.vector2.x - corridorOffset, roomPosition.vector2.y);
                    break;
                case Direction.Down: corridorPosition = new Vector2(roomPosition.vector2.x, roomPosition.vector2.y + corridorOffset);
                    break;
                case Direction.Left: corridorPosition = new Vector2(roomPosition.vector2.x + corridorOffset, roomPosition.vector2.y);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (roomPosition.direction) {
                case Direction.Down:
                case Direction.Top:
                    return Object.Instantiate(verticalCorridorPrefab, corridorPosition, Quaternion.identity);
                case Direction.Left:
                case Direction.Right:
                    return Object.Instantiate(horizontalCorridorPrefab, corridorPosition, Quaternion.identity);
            }
            return null;
        }

        private static RoomPosition GetNextRoomPosition(RoomPosition currentPosition) {
            RoomPosition positionToReturn;

            do {
                var randomDirection = (Direction)Mathf.Pow(2, Random.Range(0, 3));

                switch (randomDirection) {
                    case Direction.Top:
                        positionToReturn = new RoomPosition(new Vector2(currentPosition.vector2.x, currentPosition.vector2.y + roomOffset),
                            Direction.Top);
                        break;
                    case Direction.Right:
                        positionToReturn = new RoomPosition(new Vector2(currentPosition.vector2.x + roomOffset, currentPosition.vector2.y),
                            Direction.Right);
                        break;
                    case Direction.Down:
                        positionToReturn = new RoomPosition(new Vector2(currentPosition.vector2.x, currentPosition.vector2.y - roomOffset),
                            Direction.Down);
                        break;
                    case Direction.Left:
                        positionToReturn = new RoomPosition(new Vector2(currentPosition.vector2.x - roomOffset, currentPosition.vector2.y),
                            Direction.Left);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            } while (roomPositions.Contains(positionToReturn.vector2));

            return positionToReturn;
        }
    }
}