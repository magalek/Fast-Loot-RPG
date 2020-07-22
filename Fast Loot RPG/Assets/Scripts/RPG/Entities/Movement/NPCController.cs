using System;
using UnityEngine;

namespace RPG.Entities.Movement {
    public class NPCController : MonoBehaviour, IMoveable {
        public event Action OnMovementStart;
        public event Action OnMovementEnd;
        public bool IsMoving { get; set; } = false;
        public bool CanMove { get; set; }

        public void Move(float speed, Vector3 destination) {
            throw new System.NotImplementedException();
        }
    }
}