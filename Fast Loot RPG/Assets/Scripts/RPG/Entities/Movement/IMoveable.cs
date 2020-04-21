using UnityEngine;

namespace RPG.Entities.Movement {
    public interface IMoveable {
        void Move(float speed, Vector3 destination);
    }
}