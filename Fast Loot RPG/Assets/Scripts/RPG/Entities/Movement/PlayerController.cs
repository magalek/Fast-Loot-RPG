using System;
using RPG.UI;
using UnityEngine;

namespace RPG.Entities.Movement {
    public class PlayerController : MonoBehaviour, IMoveable {

        private Transform cameraTransform;
        private Rigidbody2D rigidbody2D => GetComponent<Rigidbody2D>();
        
        private void Start() {
            cameraTransform = MainCamera.Instance.transform;
        }

        private void FixedUpdate() {
            Move();
        }

        private void Update() {
            CenterCamera();
            
            if (Input.GetMouseButtonDown(0)) 
                Attack();
        }

        private void Attack() {
            
            Player.Instance.animationController.RotateWeaponSprite();
            Player.Instance.animationController.PlayAttack();
        }

        private void CenterCamera() {
            var position = transform.position;
            cameraTransform.position = new Vector3(position.x, position.y, -10);
        }

        public void Move() {
            float xAxis = Input.GetAxis("Horizontal");
            float yAxis = Input.GetAxis("Vertical");
            
            if (xAxis != 0 || yAxis != 0) {
                if (xAxis > 0) Player.Instance.animationController.FlipSpriteX(false);
                if (xAxis < 0) Player.Instance.animationController.FlipSpriteX(true);

                Vector3 axisMovement 
                    = new Vector3(xAxis * 0.01f, yAxis * 0.01f);
                
                Player.Instance.animationController.SetIsRunning(true);
                
                rigidbody2D.MovePosition(transform.position += axisMovement);
            }
            else
                Player.Instance.animationController.SetIsRunning(false);
        }
    }
}