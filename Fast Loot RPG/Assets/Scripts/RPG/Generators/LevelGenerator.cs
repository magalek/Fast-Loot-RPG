using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RPG.Environment;
using RPG.Utility;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace RPG.Generators {
    public static class LevelGenerator {
        public static event Action GenerationCompleted;
        private static GameObject horizontalCorridorPrefab => Resources.Load<GameObject>("Prefabs/Environment Prefabs/Corridor Horizontal");
        private static GameObject verticalCorridorPrefab => Resources.Load<GameObject>("Prefabs/Environment Prefabs/Corridor Vertical");

        private static List<Vector2> roomPositions = new List<Vector2>();

        private static float roomOffset = 2;

        private static Transform levelParent;

        public static void Init() {
            levelParent = GameObject.Find("Level").transform;
            RoomGenerator.Init();
        }
        
        public static IEnumerator GenerateLevel(int roomAmount, float distance) {
            RoomPosition currentPosition;
            RoomPosition previousPosition = RoomPosition.Zero();
            RoomPosition nextPosition = RoomPosition.Zero();

            for (int i = 0; i < roomAmount - 1; i++) {
                currentPosition = nextPosition;
                if (i < roomAmount - 2)
                    nextPosition = GetNextRoomPosition(currentPosition, distance);
                
                var room = RoomGenerator.CreateRoom(previousPosition, currentPosition, nextPosition);
                room.transform.SetParent(levelParent);
                roomPositions.Add(currentPosition.vector2);
                
                if (i < roomAmount - 2)
                    RoomPosition.positions.Add(nextPosition);
                previousPosition = currentPosition;
                //yield return new WaitForSeconds(1f);
            }

            foreach (var position in RoomPosition.positions) {
                var corridor = CreateCorridor(position);
                corridor.transform.SetParent(levelParent);
                //yield return new WaitForSeconds(1f);
            }
            
            yield return null;
            GenerationCompleted?.Invoke();
        }

        private static GameObject CreateCorridor(RoomPosition roomPosition) {
            Vector2 corridorPosition;

            float corridorOffset = roomOffset / 2;
            
            switch (roomPosition.direction) {
                case Direction.Top:
                    corridorPosition = new Vector2(roomPosition.vector2.x, roomPosition.vector2.y - corridorOffset);
                    break;
                case Direction.Right:
                    corridorPosition = new Vector2(roomPosition.vector2.x - corridorOffset, roomPosition.vector2.y);
                    break;
                case Direction.Down:
                    corridorPosition = new Vector2(roomPosition.vector2.x, roomPosition.vector2.y + corridorOffset);
                    break;
                case Direction.Left:
                    corridorPosition = new Vector2(roomPosition.vector2.x + corridorOffset, roomPosition.vector2.y);
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

        private static RoomPosition GetNextRoomPosition(RoomPosition currentPosition, float distance = 6) {
            RoomPosition positionToReturn;

            do {
                var randomDirection = (Direction)Random.Range(1, 4);

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