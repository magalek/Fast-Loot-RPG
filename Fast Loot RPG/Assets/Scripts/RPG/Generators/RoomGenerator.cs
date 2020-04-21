using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RPG.Environment;
using RPG.Utility;
using Object = UnityEngine.Object;

namespace RPG.Generators {
    public static class RoomGenerator {
        
        private static List<GameObject> roomPrefabs = new List<GameObject>();

        public static void Init() {
            roomPrefabs = Resources.LoadAll<GameObject>("Prefabs/Environment Prefabs/Rooms").ToList();
        }

        public static GameObject CreateRoom(RoomPosition currentPosition, RoomPosition nextPosition) {
            RoomOpenings nextOpenings = (RoomOpenings) nextPosition.direction;
            RoomOpenings currentOpenings = 0;
            
            // we have to assign the opposite value, because
            // current position direction tells us from which direction we came
            // so if we have an opening on the top in the previous room
            // we have to have an opening down in the current 
            switch (currentPosition.direction) {
                case Direction.Top: currentOpenings = (RoomOpenings) Direction.Down; break;
                case Direction.Right: currentOpenings = (RoomOpenings) Direction.Left; break;
                case Direction.Down: currentOpenings = (RoomOpenings) Direction.Top; break;
                case Direction.Left: currentOpenings = (RoomOpenings) Direction.Right; break;
            }

            RoomOpenings desiredOpenings;
            
            // checks if this is the last room to generate
            if (nextPosition == currentPosition)
                desiredOpenings = currentOpenings;
            else
                desiredOpenings = nextOpenings | currentOpenings;

            return Object.Instantiate(
                roomPrefabs.First(g => g.GetComponent<Room>().roomOpenings == desiredOpenings), 
                currentPosition.vector2, 
                Quaternion.identity);
        }
    }
}