using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Environment;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace RPG.Controllers {
    public static class LevelController {
        private const float Scale = 0.125f;
        private static GameObject roomPrefab => Resources.Load<GameObject>("Prefabs/Environment Prefabs/Room");
        private static GameObject corridorPrefab => Resources.Load<GameObject>("Prefabs/Environment Prefabs/Corridor");

        private static List<Vector2> points = new List<Vector2>();
        
        private static bool recentlyAddedRoom = false;

        public static IEnumerator GenerateLevel(int roomAmount, float distance) {

            RoomPosition lastPosition = RoomPosition.zero();

            for (int i = 0; i < roomAmount; i++) {
                Object.Instantiate(roomPrefab, lastPosition.vector2, Quaternion.identity);
                lastPosition = GetNextPosition(lastPosition, distance);
                yield return new WaitForSeconds(0.5f);
            }
            yield return null;
        }

        private static RoomPosition GetNextPosition(RoomPosition previousPosition, float distance = 6) {
            Direction randomDirection;
            do {
                randomDirection = (Direction)Random.Range(1, 4);
            } while (randomDirection == previousPosition.direction);

            switch (randomDirection) {
                case Direction.Top:
                    return new RoomPosition(new Vector2(previousPosition.vector2.x, previousPosition.vector2.y + 1),
                        Direction.Top);
                case Direction.Right:
                    return new RoomPosition(new Vector2(previousPosition.vector2.x + 1, previousPosition.vector2.y),
                        Direction.Right);
                case Direction.Down:
                    return new RoomPosition(new Vector2(previousPosition.vector2.x, previousPosition.vector2.y - 1),
                        Direction.Down);
                case Direction.Left:
                    return new RoomPosition(new Vector2(previousPosition.vector2.x - 1, previousPosition.vector2.y),
                        Direction.Left);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public struct RoomPosition {
            public Vector2 vector2;
            public Direction direction;

            public RoomPosition(Vector2 _vector2, Direction _direction) {
                vector2 = _vector2;
                direction = _direction;
            }

            public static RoomPosition zero() {
                return new RoomPosition(Vector2.zero, Direction.None);
            }
        }
    }
}

public enum Direction {
    None = 0,
    Top = 1,
    Right = 2,
    Down = 3,
    Left = 4
}