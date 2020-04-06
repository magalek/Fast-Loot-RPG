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
            public static List<GameObject> All;

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
                All = Resources.LoadAll<GameObject>("Prefabs/Environment Prefabs/Rooms").ToList();
            }
        }

        public static void Init() {
            Prefabs.Load();
        }
//TODO: Use flags functionality on enum  
        public static GameObject CreateRoom(RoomPosition previousPosition, RoomPosition currentPosition, RoomPosition nextPosition) {
            
            // If both previous and current positions are equal it means that this is the first room to create
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
            // If both next and current positions are equal it means that this is the last room to create
            else if (nextPosition == currentPosition) {
                switch (currentPosition.direction) {
                    case Direction.Top: return Object.Instantiate(Prefabs.Down.RandomObject(), currentPosition.vector2, Quaternion.identity);
                    case Direction.Right: return Object.Instantiate(Prefabs.Left.RandomObject(), currentPosition.vector2, Quaternion.identity);
                    case Direction.Down: return Object.Instantiate(Prefabs.Top.RandomObject(), currentPosition.vector2, Quaternion.identity);
                    case Direction.Left: return Object.Instantiate(Prefabs.Right.RandomObject(), currentPosition.vector2, Quaternion.identity);
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            RoomOpening opening = (RoomOpening) nextPosition.direction;

            switch (currentPosition.direction) {
                case Direction.None:
                    break;
                case Direction.Top: opening |= (RoomOpening) Direction.Down;
                    break;
                case Direction.Right: opening |= (RoomOpening) Direction.Left;
                    break;
                case Direction.Down: opening |= (RoomOpening) Direction.Top;
                    break;
                case Direction.Left: opening |= (RoomOpening) Direction.Right;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return Object.Instantiate(
                Prefabs.All.First(g => g.GetComponent<Room>().roomOpening == opening), 
                currentPosition.vector2, 
                Quaternion.identity);
        }
    }
}