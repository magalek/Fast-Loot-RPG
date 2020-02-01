using System;
using RPG.UI;
using UnityEngine;

namespace RPG.Entities.Movement {
    public class PlayerController : MonoBehaviour, IMoveable {

        private Transform cameraTransform;
        
        private void Start() {
            cameraTransform = MainCamera.Instance.transform;
        }

        private void Update() {
            Move();
            CenterCamera();
            
            if (Input.GetMouseButtonDown(0)) 
                Attack();
        }

        private void Attack() {
            
            Player.Instance.animationController.RotateWeaponSprite();
            Player.Instance.animationController.PlayAttack();
        }

        private void CenterCamera() {
            cameraTransform.position = new Vector3(transform.position.x, transform.position.y, -10);
        }

        public void Move() {
            float xAxis = Input.GetAxis("Horizontal");
            float yAxis = Input.GetAxis("Vertical");
            
            if (xAxis != 0 || yAxis != 0) {
                if (xAxis > 0) Player.Instance.animationController.FlipSpriteX(false);
                if (xAxis < 0) Player.Instance.animationController.FlipSpriteX(true);

                Vector2 axisMovement 
                    = new Vector2(xAxis * 0.01f, yAxis * 0.01f);
                
                Player.Instance.animationController.SetIsRunning(true);
                
                transform.Translate(axisMovement);
            }
            else
                Player.Instance.animationController.SetIsRunning(false);
        }
    }
}