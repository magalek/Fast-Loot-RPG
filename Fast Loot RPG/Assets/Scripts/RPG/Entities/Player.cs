using RPG.Entities.Animations;
using RPG.Entities.Movement;
using RPG.Items;
using RPG.Utility;

namespace RPG.Entities
{
    public class Player : Entity, IComponentCache, IHittable {
        public static Player Instance;

        public Equipment equipment;
        
        public PlayerController playerController;
        public PlayerAnimationController animationController;
        
        private void Awake()
        {
            base.Awake();
            
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);
            
            CacheComponents();

            DontDestroyOnLoad(gameObject);
        }

        public virtual void Hit(int damage) {
            health.Subtract(damage);
            animationController.PlayHit();
            
            if (health.zeroOrLess)
                Kill();
        }

        public void CacheComponents() {
            playerController = GetComponent<PlayerController>();
            animationController = GetComponent<PlayerAnimationController>();
        }
    }
}
