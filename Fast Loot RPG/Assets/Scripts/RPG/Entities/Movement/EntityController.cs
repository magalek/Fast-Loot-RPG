using System;
using RPG.Entities.AnimationControllers;
using UnityEngine;

namespace RPG.Entities.Movement {
    public class EntityController : MonoBehaviour, IMoveable {

        public EntityAnimationController entityAnimationController;

        private void Awake() {
            entityAnimationController = GetComponent<EntityAnimationController>();
        }

        private void Update() {
            if (Vector2.Distance(transform.position, Player.Instance.transform.position) < 1) {
                entityAnimationController.FlipSpriteX(transform.position.x > Player.Instance.transform.position.x);
                Move();
            }
            else
                entityAnimationController.SetIsRunning(false);
        }

        public void Move() {
            entityAnimationController.SetIsRunning(true);
            transform.position 
                = Vector3.MoveTowards(transform.position, Player.Instance.transform.position, 0.001f);
        }
    }
}