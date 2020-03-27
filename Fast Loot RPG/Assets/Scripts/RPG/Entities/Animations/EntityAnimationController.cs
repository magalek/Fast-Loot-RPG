using RPG.Utility;
using UnityEngine;

namespace RPG.Entities.Animations {
    public class EntityAnimationController : MonoBehaviour, IComponentCache{
        public Animator animator;
        public SpriteRenderer spriteRenderer;

        private static readonly int IsRunningId = Animator.StringToHash("IsRunning");
        private static readonly int AttackId = Animator.StringToHash("Attack");
        private static readonly int HitId = Animator.StringToHash("Hit");
        
        private void Awake() {
            CacheComponents();
        }
        
        public void CacheComponents() {
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void FlipSpriteX(bool flip) {
            spriteRenderer.flipX = flip;
        }
        
        public void SetIsRunning(bool animate) => animator.SetBool(IsRunningId, animate);
        public void PlayAttack() => animator.SetTrigger(AttackId);
        public void PlayHit() => animator.SetTrigger(HitId);
        
        
    }
}