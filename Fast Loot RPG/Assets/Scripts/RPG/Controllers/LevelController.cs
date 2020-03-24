using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RPG.Entities.AnimationControllers;
using RPG.Environment;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace RPG.Controllers {
    public static class LevelController {
        private const float Scale = 0.125f;
        private static GameObject roomPrefab => Resources.Load<GameObject>("Prefabs/Environment Prefabs/Room");
        private static GameObject horizontalCorridorPrefab => Resources.Load<GameObject>("Prefabs/Environment Prefabs/Corridor Horizontal");
        private static GameObject verticalCorridorPrefab => Resources.Load<GameObject>("Prefabs/Environment Prefabs/Corridor Vertical");

        private static List<Vector2> roomPositions = new List<Vector2>();
        
        private static bool recentlyAddedRoom = false;
        private static int directionCounter = 0;

        public static IEnumerator GenerateLevel(int roomAmount, float distance) {

            RoomPosition nextPosition = RoomPosition.Zero();

            for (int i = 0; i < roomAmount - 1; i++) {
                CreateRoom(nextPosition);
                
                nextPosition = GetNextPosition(nextPosition, distance);
                
                if (i < roomAmount - 2)
                    RoomPosition.positions.Add(nextPosition);
                yield return new WaitForSeconds(0.1f);
            }

            foreach (var position in RoomPosition.positions) {
                CreateCorridor(position);
                yield return new WaitForSeconds(0.1f);
            }
            yield return null;
        }

        private static void CreateRoom(RoomPosition nextPosition) {
            Object.Instantiate(roomPrefab, nextPosition.vector2, Quaternion.identity);
            roomPositions.Add(nextPosition.vector2);
        }

        private static void CreateCorridor(RoomPosition roomPosition) {
            Vector2 corridorPosition;
            
            switch (roomPosition.direction) {
                case Direction.Top:
                    corridorPosition = new Vector2(roomPosition.vector2.x, roomPosition.vector2.y - 0.5f);
                    break;
                case Direction.Right:
                    corridorPosition = new Vector2(roomPosition.vector2.x - 0.5f, roomPosition.vector2.y);
                    break;
                case Direction.Down:
                    corridorPosition = new Vector2(roomPosition.vector2.x, roomPosition.vector2.y + 0.5f);
                    break;
                case Direction.Left:
                    corridorPosition = new Vector2(roomPosition.vector2.x + 0.5f, roomPosition.vector2.y);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (roomPosition.direction) {
                case Direction.Down:
                case Direction.Top:
                    Object.Instantiate(verticalCorridorPrefab, corridorPosition, Quaternion.identity);
                    break;
                case Direction.Left:
                case Direction.Right:
                    Object.Instantiate(horizontalCorridorPrefab, corridorPosition, Quaternion.identity);
                    break;
            }
        }

        private static RoomPosition GetNextPosition(RoomPosition previousPosition, float distance = 6) {
            Direction randomDirection;
            RoomPosition positionToReturn;
            
            do {
                randomDirection = (Direction)Random.Range(1, 4);
                
                switch (randomDirection) {
                    case Direction.Top:
                        positionToReturn = new RoomPosition(new Vector2(previousPosition.vector2.x, previousPosition.vector2.y + 1),
                            Direction.Top);
                        break;
                    case Direction.Right:
                        positionToReturn = new RoomPosition(new Vector2(previousPosition.vector2.x + 1, previousPosition.vector2.y),
                            Direction.Right);
                        break;
                    case Direction.Down:
                        positionToReturn = new RoomPosition(new Vector2(previousPosition.vector2.x, previousPosition.vector2.y - 1),
                            Direction.Down);
                        break;
                    case Direction.Left:
                        positionToReturn = new RoomPosition(new Vector2(previousPosition.vector2.x - 1, previousPosition.vector2.y),
                            Direction.Left);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            } while (roomPositions.Contains(positionToReturn.vector2));

            if (previousPosition.direction == randomDirection)
                directionCounter++;
            else
                directionCounter = 0;
            
            return positionToReturn;
        }

        public struct RoomPosition {
            public static List<RoomPosition> positions = new List<RoomPosition>();
            
            public Vector2 vector2;
            public Direction direction;
            
            public RoomPosition(Vector2 _vector2, Direction _direction) {
                vector2 = _vector2;
                direction = _direction;
            }

            public static RoomPosition Zero() {
                return new RoomPosition(Vector2.zero, Direction.None);
            }
            
            
        }
    }
}