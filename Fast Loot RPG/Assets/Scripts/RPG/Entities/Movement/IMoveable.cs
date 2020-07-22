using System;
using UnityEngine;

namespace RPG.Entities.Movement {
    public interface IMoveable {

        event Action OnMovementStart;

        event Action OnMovementEnd;

        bool IsMoving { get; set; }

        bool CanMove { get; set; }
        
        void Move(float speed, Vector3 destination = default);
    }
}