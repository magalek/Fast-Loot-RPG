using System;
using RPG.UI;
using UnityEngine;

namespace RPG.Entities.Movement {
    public class PlayerController : MonoBehaviour, IMoveable {
        
        public bool isMoving = false;
        
        private void FixedUpdate() {
            Move();
        }

        private void Update() {
            if (Input.GetMouseButtonDown(0)) 
                Attack();
        }

        private void Attack() {
            Player.Instance.animationController.RotateWeaponSprite();
            Player.Instance.animationController.PlayAttack();
        }

        public void Move() {
            float xAxis = Input.GetAxisRaw("Horizontal");
            float yAxis = Input.GetAxisRaw("Vertical");
            
            if (xAxis != 0 || yAxis != 0) {
                isMoving = true;
                if (xAxis > 0) Player.Instance.animationController.FlipSpriteX(false);
                if (xAxis < 0) Player.Instance.animationController.FlipSpriteX(true);

                Vector3 movementVector 
                    = new Vector3(xAxis, yAxis);
                movementVector.Normalize();
                
                Player.Instance.animationController.SetIsRunning(true);
                
                transform.Translate(movementVector * (Time.deltaTime * 0.7f));
                MainCamera.Instance.Center(transform, 0.1f);
            }
            else {
                isMoving = false;
                Player.Instance.animationController.SetIsRunning(false);
            }
        }
    }
}