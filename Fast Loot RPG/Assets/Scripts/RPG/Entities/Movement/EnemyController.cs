using System;
using System.Collections;
using RPG.Entities.Animations;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RPG.Entities.Movement {
    public class EnemyController : MonoBehaviour, IMoveable {

        [Range(0.1f, 1f)] [SerializeField] private float moveSpeed = 0.3f; 
        
        public EntityAnimationController entityAnimationController;

        private Vector3 positionBeforePatrolling;
        private Vector3 patrolDestination;
        
        private Transform attackTarget;

        private bool isPushed = false;

        private void Awake() {
            entityAnimationController = GetComponent<EntityAnimationController>();

            Player.Spawned += SetTarget;

            positionBeforePatrolling = transform.position;
            StartCoroutine(Patrol());
        }

        private void Update() {
            if (TargetInRange() && !isPushed) {
                positionBeforePatrolling = transform.position;
                Move(moveSpeed, attackTarget.position);
            }
        }

        private bool TargetInRange() {
            if (attackTarget == null) return false;
            
            if (Vector2.Distance(transform.position, attackTarget.position) < 1)
                return true;
            entityAnimationController.SetIsRunning(false);
            return false;
        }

        private void SetTarget() {
            attackTarget = Player.Instance.transform;
        }
        
        public void Move(float speed, Vector3 destination) {
            var position = transform.position;
            entityAnimationController.FlipSpriteX(position.x > destination.x);
            entityAnimationController.SetIsRunning(true);
            position = Vector3.MoveTowards(position, destination, speed * Time.deltaTime) ;
            transform.position = position;
        }

        public IEnumerator Pushback() {
            isPushed = true;
            Vector3 offset = transform.position - attackTarget.position;
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
                }
                yield return new WaitWhile(TargetInRange);
            }
        }
    }
}