using System.Collections.Generic;
using UnityEngine;

namespace RPG.Environment {
    public struct RoomPosition {
        public static List<RoomPosition> positions = new List<RoomPosition>();
            
        public Vector2 vector2;
        public Direction direction;
            
        public RoomPosition(Vector2 _vector2, Direction _direction) {
            vector2 = _vector2;
            direction = _direction;
        }
        
        public RoomPosition(Vector2 _vector2) {
            vector2 = _vector2;
            direction = Direction.None;
        }

        public static RoomPosition Zero() {
            return new RoomPosition(Vector2.zero, Direction.None);
        }
            
        public static bool operator== (RoomPosition a, RoomPosition b) {
            return (a.vector2 == b.vector2) && (a.direction == b.direction);
        }

        public static bool operator !=(RoomPosition a, RoomPosition b) {
            return !(a == b);
        }
    }
}