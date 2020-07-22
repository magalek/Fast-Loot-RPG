using System;
using System.Collections;
using RPG.Entities.Animations;
using RPG.Utility;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RPG.Entities.Movement {
    public class EnemyController : MonoBehaviour, IMoveable {

        [Range(0.1f, 1f)] [SerializeField] private float moveSpeed = 0.3f;
        [SerializeField] private CharacterType type;
        
        public EntityAnimationController entityAnimationController;

        public event Action OnMovementStart;
        public event Action OnMovementEnd;
        public bool IsMoving { get; set; }
        public bool CanMove { get; set; }

        private Vector3 positionBeforePatrolling;
        private Vector3 patrolDestination;
        
        private Transform attackTargetTransform;

        private Cooldown<EnemyController> shootingCooldown;
        
        private bool isPushed = false;
        private bool shootingOffCooldown = true;

        private void Awake() {
            entityAnimationController = GetComponent<EntityAnimationController>();

            Player.Spawned += SetTarget;

            positionBeforePatrolling = transform.position;
            shootingCooldown = new Cooldown<EnemyController>(this, 2, (e) => e.shootingOffCooldown = true);

            StartCoroutine(Patrol());
        }

        private void Update() {
            AttackTarget();
        }

        private void AttackTarget() {
            switch (type) {
                case CharacterType.Melee:
                    if (TargetInRange() && !isPushed) {
                        positionBeforePatrolling = transform.position;
                        Move(moveSpeed, attackTargetTransform.position);
                    }

                    break;
                case CharacterType.Ranged:
                    if (TargetInRange() && !isPushed && TargetInLineOfSight() && shootingOffCooldown) {
                        positionBeforePatrolling = transform.position;
                        Shoot();
                    }
                    break;
            }
        }

        private void Shoot() {
            shootingOffCooldown = false;
            entityAnimationController.FlipSpriteX(transform.position.x > attackTargetTransform.position.x);
            entityAnimationController.SetIsRunning(true);
            
            GameObject projectileObject = Instantiate(Resources.Load<GameObject>("Prefabs/Bat Projectile"), transform.position, Quaternion.identity);
            Projectile projectile = projectileObject.GetComponent<Projectile>();
            projectile.SetTarget(attackTargetTransform.position);
            shootingCooldown.Start();
        }

        private bool TargetInRange() {
            if (attackTargetTransform == null) return false;
            
            if (Vector2.Distance(transform.position, attackTargetTransform.position) < 1)
                return true;
            entityAnimationController.SetIsRunning(false);
            return false;
        }

        private bool TargetInLineOfSight() {
            var currentPosition = transform.position;
            if (attackTargetTransform != null) {
                var targetPosition = attackTargetTransform.transform.position;
                RaycastHit2D[] hits = Physics2D.RaycastAll(currentPosition, targetPosition - currentPosition, Vector2.Distance(currentPosition, targetPosition));
            
                foreach (var hit in hits) {
                    if (hit.collider != null) {
                        if (hit.transform.CompareTag("Player") == false && 
                            hit.transform.CompareTag("Enemy") == false && 
                            hit.transform.CompareTag("Item") == false) {
                            
                            Debug.DrawRay(currentPosition, targetPosition - currentPosition, Color.red);
                            return false;
                        }
                    }
                }
            
                Debug.DrawRay(currentPosition, targetPosition - currentPosition, Color.green);
            }

            return true;
        }

        private void SetTarget() {
            attackTargetTransform = Player.Instance.transform;
        }

        public void Move(float speed, Vector3 destination) {
            IsMoving = true;
            var position = transform.position;
            entityAnimationController.FlipSpriteX(position.x > destination.x);
            entityAnimationController.SetIsRunning(true);
            position = Vector3.MoveTowards(position, destination, speed * Time.deltaTime) ;
            transform.position = position;
        }

        public IEnumerator Pushback() {
            isPushed = true;
            Vector3 offset = transform.position - attackTargetTransform.position;
            Vector3 finalPosition = transform.position + offset / 5 ;

            while (transform.position != finalPosition) {
                transform.position = Vector2.Lerp(transform.position ,finalPosition, 0.1f);
            }
            
            isPushed = false;
            yield return null;
        }
        
        private IEnumerator Patrol() {
            while (true) {
                if (!TargetInRange()) {
                    Vector3 randomPosition = new Vector3(
                        positionBeforePatrolling.x + Random.Range(-1f, 1f), 
                        positionBeforePatrolling.y + Random.Range(-1f, 1f));

                    float patrolTime = 0;
                    
                    while (!TargetInRange()) {
                        if (patrolTime > 5)
                            break;
                        patrolTime += Time.deltaTime;
                        
                        Move(0.1f, randomPosition);
                        
                        yield return new WaitForSeconds(Time.deltaTime);
                    }

                    IsMoving = false;
                }
                yield return new WaitWhile(TargetInRange);
            }
        }
    }
}