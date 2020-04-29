using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RPG.Materials;
using RPG.UI;
using RPG.Utility;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

namespace RPG.Entities.Movement {
    public class PlayerController : MonoBehaviour, IMoveable {

        private float dashSpeed = 2;

        private Cooldown dashCooldown = new Cooldown(2);
        
        public bool isMoving = false;
        
        private bool canDash = true;
        private bool canMove = true;
        
        private float xAxisMovement;
        private float yAxisMovement;

        private PlayerMaterial playerMaterial;

        private void Awake() {
            playerMaterial = GetComponent<PlayerMaterial>();
            dashCooldown.Ended += () => canDash = true;
        }

        private void Update() {
            xAxisMovement = Input.GetAxisRaw("Horizontal");
            yAxisMovement = Input.GetAxisRaw("Vertical");

            if (canMove)
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
            
            Vector2 direction 
                = new Vector2(xAxisMovement, yAxisMovement);
            direction.Normalize();
            
            direction /= 2f;

            Vector2 destination = (Vector2)transform.position + direction;
            List<RaycastHit2D> hits = 
                Physics2D.RaycastAll(transform.position, direction, Vector3.Magnitude(direction)).ToList();
            
            foreach (var hit in hits) {
                if (!hit.transform.gameObject.CompareTag("Player")) {
                    destination = hit.point - direction / 20;
                    break;
                }
            }
            StartCoroutine(DashCoroutine(destination, dashSpeed));
            dashCooldown.Start();
        }

        private IEnumerator DashCoroutine(Vector2 destination, float speed = 1) {
            canMove = false;

            float elapsedTime = 0;
            Vector2 origin = transform.position;
            float distance = Vector2.Distance(origin, destination);
            
            playerMaterial.Set(PlayerMaterial.BlurDir, (destination - origin).normalized / 10);
            
            while ((Vector2)transform.position != destination) {
                transform.position = Vector2.Lerp(origin, destination, Mathf.Tan(elapsedTime / distance)   * speed);
                MainCamera.Instance.Center(transform, 0.06f);
                
                playerMaterial.Set(PlayerMaterial.BlurAmount, elapsedTime * 50);
                elapsedTime += Time.deltaTime;
                yield return 1;
            }
            playerMaterial.Set(PlayerMaterial.BlurAmount, 0);

            playerMaterial.Set(PlayerMaterial.BlurDir, Vector4.zero);
            
            canMove = true;
            yield return null;
        }
    }
}