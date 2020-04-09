using System;
using RPG.Entities.Animations;
using UnityEngine;

namespace RPG.Entities.Movement {
    public class EnemyController : MonoBehaviour, IMoveable {

        public EntityAnimationController entityAnimationController;
        private Transform target = null; 
        
        private Rigidbody2D rigidbody2D;
        
        private void Awake() {
            entityAnimationController = GetComponent<EntityAnimationController>();

            Player.Spawned += SetTarget;
            
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update() {
            if (TargetInRange()) {
                Move();
            }
        }

        private bool TargetInRange() {
            if (target == null) return false;
            
            if (Vector2.Distance(transform.position, target.position) < 1)
                return true;
            entityAnimationController.SetIsRunning(false);
            return false;
        }

        private void SetTarget() {
            target = Player.Instance.transform;
        }
        
        public void Move() {
            var position = transform.position;
            entityAnimationController.FlipSpriteX(position.x > target.position.x);
            entityAnimationController.SetIsRunning(true);
            position = Vector3.MoveTowards(position, target.position, 0.3f * Time.deltaTime) ;
            transform.position = position;
        }

        private void Patrol() {
            
        }
    }
}