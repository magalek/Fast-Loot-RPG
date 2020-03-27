using System;
using RPG.Entities.Animations;
using UnityEngine;

namespace RPG.Entities.Movement {
    public class EntityController : MonoBehaviour, IMoveable {

        public EntityAnimationController entityAnimationController;
        private Transform target; 
        
        private void Awake() {
            entityAnimationController = GetComponent<EntityAnimationController>();
            target = Player.Instance.transform;
        }

        private void Update() {
            if (TargetInRange()) {
                Move();
            }
        }

        private bool TargetInRange() {
            if (Vector2.Distance(transform.position, target.position) < 1)
                return true;
            entityAnimationController.SetIsRunning(false);
            return false;
        }

        public void Move() {
            entityAnimationController.FlipSpriteX(transform.position.x > target.position.x);
            entityAnimationController.SetIsRunning(true);
            transform.position 
                = Vector3.MoveTowards(transform.position, target.position, 0.001f);
        }
    }
}