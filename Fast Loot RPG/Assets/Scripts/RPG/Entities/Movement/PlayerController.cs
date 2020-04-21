using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RPG.UI;
using UnityEngine;

namespace RPG.Entities.Movement {
    public class PlayerController : MonoBehaviour, IMoveable {

        private float dashCooldownTime = 2;
        
        public bool isMoving = false;
        
        private bool canDash = true;
        
        private float xAxisMovement;
        private float yAxisMovement;
        
        private void Update() {
            xAxisMovement = Input.GetAxisRaw("Horizontal");
            yAxisMovement = Input.GetAxisRaw("Vertical");


            Move(0.7f, Vector2.zero);
            
            if (Input.GetMouseButtonDown(0)) 
                Player.Instance.weapon.Attack();
            
            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash) {
                Dash();
            }
        }

        public void Move(float speed, Vector3 destination) {
            if (xAxisMovement != 0 || yAxisMovement != 0) {
                isMoving = true;
                if (xAxisMovement > 0) Player.Instance.animationController.FlipSpriteX(false);
                if (xAxisMovement < 0) Player.Instance.animationController.FlipSpriteX(true);

                Vector2 movementVector 
                    = new Vector2(xAxisMovement, yAxisMovement);
                movementVector.Normalize();
                
                Player.Instance.animationController.SetIsRunning(true);
                
                transform.Translate(movementVector * (Time.deltaTime * speed));
                MainCamera.Instance.Center(transform, 0.1f);
            }
            else {
                isMoving = false;
                Player.Instance.animationController.SetIsRunning(false);
            }
        }

        private void Dash() {
            canDash = false;
            
            Vector2 movementVector 
                = new Vector2(xAxisMovement, yAxisMovement);
            movementVector.Normalize();

            Vector2 positionToDash = movementVector / 5;
            
            List<RaycastHit2D> hits = 
                Physics2D.RaycastAll(transform.position, movementVector, Vector3.Magnitude(movementVector / 5)).ToList();

            foreach (var hit in hits) {
                if (!hit.transform.gameObject.CompareTag("Player")) {
                    positionToDash = hit.point - (Vector2)transform.position;
                    Debug.Log(positionToDash);
                    break;
                }

                Debug.Log(hit.transform.name);
            }
            
            transform.Translate(positionToDash);
            
            
            StartCoroutine(DashCooldown());
        }

        private IEnumerator DashCooldown() {
            yield return new WaitForSeconds(dashCooldownTime);
            canDash = true;
        }
    }
}