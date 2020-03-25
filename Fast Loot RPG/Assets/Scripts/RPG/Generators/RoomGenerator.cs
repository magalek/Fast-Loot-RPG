using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RPG.Environment;
using RPG.Utility;
using Object = UnityEngine.Object;

namespace RPG.Generators {
    public static class RoomGenerator {
        private static class Prefabs {
            public static List<GameObject> Top;
            public static List<GameObject> Right;
            public static List<GameObject> Down;
            public static List<GameObject> Left;
            public static List<GameObject> DownLeft;
            public static List<GameObject> DownTop;
            public static List<GameObject> DownRight;
            public static List<GameObject> LeftRight;
            public static List<GameObject> LeftTop;
            public static List<GameObject> TopRight;

            public static void Load() {
                Top = Resources.LoadAll<GameObject>("Prefabs/Environment Prefabs/Rooms/Top").ToList();
                Right = Resources.LoadAll<GameObject>("Prefabs/Environment Prefabs/Rooms/Right").ToList();
                Down = Resources.LoadAll<GameObject>("Prefabs/Environment Prefabs/Rooms/Down").ToList();
                Left = Resources.LoadAll<GameObject>("Prefabs/Environment Prefabs/Rooms/Left").ToList();
                DownLeft = Resources.LoadAll<GameObject>("Prefabs/Environment Prefabs/Rooms/Down Left").ToList();
                DownTop = Resources.LoadAll<GameObject>("Prefabs/Environment Prefabs/Rooms/Down Top").ToList();
                DownRight = Resources.LoadAll<GameObject>("Prefabs/Environment Prefabs/Rooms/Down Right").ToList();
                LeftRight = Resources.LoadAll<GameObject>("Prefabs/Environment Prefabs/Rooms/Left Right").ToList();
                LeftTop = Resources.LoadAll<GameObject>("Prefabs/Environment Prefabs/Rooms/Left Top").ToList();
                TopRight = Resources.LoadAll<GameObject>("Prefabs/Environment Prefabs/Rooms/Top Right").ToList();
            }
        }

        public static void Init() {
            Prefabs.Load();
        }

        public static GameObject CreateRoom(RoomPosition previousPosition, RoomPosition currentPosition, RoomPosition nextPosition) {
            if (previousPosition == currentPosition) {
                switch (nextPosition.direction) {
                    case Direction.Top: return Object.Instantiate(Prefabs.Top.RandomObject(), currentPosition.vector2, Quaternion.identity);
                    case Direction.Right: return Object.Instantiate(Prefabs.Right.RandomObject(), currentPosition.vector2, Quaternion.identity);
                    case Direction.Down: return Object.Instantiate(Prefabs.Down.RandomObject(), currentPosition.vector2, Quaternion.identity);
                    case Direction.Left: return Object.Instantiate(Prefabs.Left.RandomObject(), currentPosition.vector2, Quaternion.identity);
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            if (nextPosition == currentPosition) {
                switch (currentPosition.direction) {
                    case Direction.Top: return Object.Instantiate(Prefabs.Down.RandomObject(), currentPosition.vector2, Quaternion.identity);
                    case Direction.Right: return Object.Instantiate(Prefabs.Left.RandomObject(), currentPosition.vector2, Quaternion.identity);
                    case Direction.Down: return Object.Instantiate(Prefabs.Top.RandomObject(), currentPosition.vector2, Quaternion.identity);
                    case Direction.Left: return Object.Instantiate(Prefabs.Right.RandomObject(), currentPosition.vector2, Quaternion.identity);
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            switch (currentPosition.direction) {
                case Direction.Top:
                    switch (nextPosition.direction) {
                        case Direction.Top: return Object.Instantiate(Prefabs.DownTop.RandomObject(), currentPosition.vector2, Quaternion.identity);
                        case Direction.Right:return Object.Instantiate(Prefabs.DownRight.RandomObject(), currentPosition.vector2, Quaternion.identity);
                        case Direction.Left:return Object.Instantiate(Prefabs.DownLeft.RandomObject(), currentPosition.vector2, Quaternion.identity);
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                case Direction.Right:
                    switch (nextPosition.direction) {
                        case Direction.Top:return Object.Instantiate(Prefabs.LeftTop.RandomObject(), currentPosition.vector2, Quaternion.identity);
                        case Direction.Right:return Object.Instantiate(Prefabs.LeftRight.RandomObject(), currentPosition.vector2, Quaternion.identity);
                        case Direction.Down:return Object.Instantiate(Prefabs.DownLeft.RandomObject(), currentPosition.vector2, Quaternion.identity);
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                case Direction.Down:
                    switch (nextPosition.direction) {
                        case Direction.Right:return Object.Instantiate(Prefabs.TopRight.RandomObject(), currentPosition.vector2, Quaternion.identity);
                        case Direction.Down:return Object.Instantiate(Prefabs.DownTop.RandomObject(), currentPosition.vector2, Quaternion.identity);
                        case Direction.Left:return Object.Instantiate(Prefabs.LeftTop.RandomObject(), currentPosition.vector2, Quaternion.identity);
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                case Direction.Left:
                    switch (nextPosition.direction) {
                        case Direction.Top:return Object.Instantiate(Prefabs.TopRight.RandomObject(), currentPosition.vector2, Quaternion.identity);
                        case Direction.Down:return Object.Instantiate(Prefabs.DownRight.RandomObject(), currentPosition.vector2, Quaternion.identity);
                        case Direction.Left:return Object.Instantiate(Prefabs.LeftRight.RandomObject(), currentPosition.vector2, Quaternion.identity);
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}