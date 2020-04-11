using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RPG.Controllers;
using RPG.Environment;
using RPG.Utility;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace RPG.Generators {
    public static class LevelGenerator {
        public static event Action GenerationCompleted;
        
        public static event Action LevelRestarted;

        public static bool Initialised = false;

        public static int DefaultRoomAmount = 20;
        private static GameObject horizontalCorridorPrefab => Resources.Load<GameObject>("Prefabs/Environment Prefabs/Corridor Horizontal");
        private static GameObject verticalCorridorPrefab => Resources.Load<GameObject>("Prefabs/Environment Prefabs/Corridor Vertical");

        private static List<Vector2> roomPositions = new List<Vector2>();

        private static float roomOffset = 2;

        private static Transform levelParent;
        
        private static Transform environmentParent;

        private static Transform enemiesParent;
        
        private static List<Room> rooms = new List<Room>();
        
        public static void Init() {
            RoomGenerator.Init();
            Initialised = true;
        }
        
        public static void GenerateLevel(int roomAmount = 0) {
            if (roomAmount == 0) roomAmount = DefaultRoomAmount; 
            
            levelParent = new GameObject("Level").transform;
            
            environmentParent = new GameObject("Environment").transform;
            environmentParent.SetParent(levelParent);
            
            enemiesParent = new GameObject("Enemies").transform;
            enemiesParent.SetParent(levelParent);
            
            GenerateRooms(roomAmount);

            GenerateCorridors();

            GenerateEnemies(0, 4);

            GenerationCompleted?.Invoke();
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
            RoomPosition previousPosition = RoomPosition.Zero();
            RoomPosition nextPosition = RoomPosition.Zero();

            for (int i = 0; i < amount - 1; i++) {
                var currentPosition = nextPosition;
                if (i < amount - 2)
                    nextPosition = GetNextRoomPosition(currentPosition);

                var room = RoomGenerator.CreateRoom(previousPosition, currentPosition, nextPosition);
                rooms.Add(room.GetComponent<Room>());
                room.name = $"Room {i}";
                room.transform.SetParent(environmentParent);
                roomPositions.Add(currentPosition.vector2);

                if (i < amount - 2)
                    RoomPosition.positions.Add(nextPosition);
                previousPosition = currentPosition;
            }
            
            rooms.RemoveAt(0);
        }

        public static void ClearLevel() {
            Object.Destroy(levelParent.gameObject);
            roomPositions.Clear();
            RoomPosition.positions.Clear();
            rooms.Clear();
        }

        private static void GenerateEnemies(int minAmount, int maxAmount) {
            for (int i = 1; i < rooms.Count; i++) {
                int count = Random.Range(minAmount, maxAmount);
                            
                List<SpawnPoint> spawnPoints = rooms[i].SpawnPoints;
    
                for (int j = 0; j < count; j++) {
                    SpawnPoint spawnPoint = spawnPoints.RandomObject();
                    spawnPoints.Remove(spawnPoint);
                    GameObject enemyPrefab = ResourcesController.enemyPrefabs.RandomObject();
                    GameObject enemy =  Object.Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity);
                    enemy.transform.SetParent(enemiesParent);
                }
            }
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