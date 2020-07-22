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
using Image = UnityEngine.UI.Image;

namespace RPG.Entities.Movement {
    public class PlayerController : MonoBehaviour, IMoveable {

        [SerializeField] private Image dashImage;
        [SerializeField] private float dashSpeed = 2;

        private Cooldown<PlayerController> dashCooldown;

        public event Action OnMovementStart;
        public event Action OnMovementEnd;

        public bool IsMoving {
            get => isMoving;
            set {
                isMoving = value;
                Player.Instance.animationController.SetIsRunning(isMoving);
            }
        }

        public bool CanDash { get; set; } = true;
        public bool CanMove { get; set; } = true;

        private float xAxisMovement;
        private float yAxisMovement;

        private PlayerMaterial playerMaterial;

        private Coroutine interactionCoroutine;
        private bool isCoroutineRunning;
        private bool isMoving = false;

        private void ChangeTest() {
            CanDash = true;
        }
        
        private void Awake() {
            playerMaterial = GetComponent<PlayerMaterial>();
            dashCooldown = new Cooldown<PlayerController>(this, 2, p => p.CanDash = true);
        }

        private void Start() {
            MainCamera.Instance.CenterOn(transform, 0.2f);
            OnMovementStart += () => Debug.Log("start");
            OnMovementEnd += () => Debug.Log("end");
        }

        private void Update() {
            Move(0.7f);
            
            if (Input.GetMouseButtonDown(0)) 
                Player.Instance.weapon.Attack();
            
            if (Input.GetKeyDown(KeyCode.LeftShift) && CanMove && CanDash) {
                Dash();
            }
            dashImage.fillAmount = dashCooldown.Percentage;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.isTrigger && isCoroutineRunning == false) {
                interactionCoroutine = StartCoroutine(InteractionCoroutine(other));
                isCoroutineRunning = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (other.isTrigger && isCoroutineRunning) {
                StopCoroutine(interactionCoroutine);
                isCoroutineRunning = false;
            }
        }

        private IEnumerator InteractionCoroutine(Collider2D other) {
            while (true) {
                if (other && !other.CompareTag("Player") && other.isTrigger) {
                    other.TryGetComponent(out IInteractable interactable);
                    interactable?.Interact(gameObject);
                }
                yield return 1;
            }
        }
        
        public void Move(float speed, Vector3 destination = default) {
            if (!CanMove) return;
            
            xAxisMovement = Input.GetAxisRaw("Horizontal");
            yAxisMovement = Input.GetAxisRaw("Vertical");
            
            if (xAxisMovement != 0 || yAxisMovement != 0) {
                if (!isMoving) OnMovementStart?.Invoke();
                IsMoving = true;
                
                Player.Instance.animationController.FlipSpriteX(xAxisMovement < 0);

                Vector2 movementVector 
                    = new Vector2(xAxisMovement, yAxisMovement);
                movementVector.Normalize();

                transform.Translate(movementVector * (Time.deltaTime * speed));
                
                MainCamera.Instance.CenterManually(transform, 0.1f);
            }
            else {
                if (isMoving) OnMovementEnd?.Invoke();
                IsMoving = false;
            }
        }

        private void Dash() {
            CanDash = false;
            
            Vector2 direction 
                = new Vector2(xAxisMovement, yAxisMovement);
            direction.Normalize();
            
            direction /= 2f;

            Vector2 destination = (Vector2)transform.position + direction;
            List<RaycastHit2D> hits = 
                Physics2D.RaycastAll(transform.position, direction, Vector3.Magnitude(direction)).ToList();
            
            foreach (var hit in hits) {
                if (!hit.transform.gameObject.CompareTag("Player") && !hit.transform.gameObject.CompareTag("Item")) {
                    destination = hit.point - direction / 20;
                    break;
                }
            }
            StartCoroutine(DashCoroutine(destination, dashSpeed));
            dashCooldown.Start();
        }

        private IEnumerator DashCoroutine(Vector2 destination, float speed = 1) {
            CanMove = false;

            float elapsedTime = 0;
            Vector2 origin = transform.position;
            float distance = Vector2.Distance(origin, destination);
            
            playerMaterial.Set(PlayerMaterial.BlurDir, (destination - origin).normalized / 10);
            
            while ((Vector2)transform.position != destination) {
                transform.position = Vector2.Lerp(origin, destination, Mathf.Tan(elapsedTime / distance)   * speed);
                MainCamera.Instance.CenterManually(transform, 0.6f);
                
                playerMaterial.Set(PlayerMaterial.BlurAmount, elapsedTime * 50);
                elapsedTime += Time.deltaTime;
                
                yield return 1;
            }
            playerMaterial.Set(PlayerMaterial.BlurAmount, 0);

            playerMaterial.Set(PlayerMaterial.BlurDir, Vector4.zero);
            
            CanMove = true;
            MainCamera.Instance.CenterOn(transform, 0.2f);
            yield return null;
        }
    }
}