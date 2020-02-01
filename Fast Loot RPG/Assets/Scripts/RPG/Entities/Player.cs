using RPG.Entities.AnimationControllers;
using RPG.Entities.Movement;

namespace RPG.Entities
{
    public class Player : Entity
    {
        public static Player Instance;
        
        public PlayerAnimationController animationController;
        public PlayerController playerController;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);

            SetTag();
            SetComponents();
            
            playerController = GetComponent<PlayerController>();
            animationController = GetComponent<PlayerAnimationController>();

            DontDestroyOnLoad(gameObject);
        }

        protected override void SetTag() {
            tag = "Player";
        }
    }
}
