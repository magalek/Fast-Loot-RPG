using RPG.Controllers;
using RPG.Entities;
using RPG.Generators;
using UnityEngine;

namespace RPG.UI.Animations {
    public class UIAnimationController : MonoBehaviour {

        private Animator animator;

        private void Awake() {
            animator = GetComponent<Animator>();
            Player.Died += PlayDeathScreen;
            Player.Died += ResetRestart;
            GameController.GameRestarted += PlayRestart;
            //Player.Spawned += ResetRestart;
        }

        private void PlayRestart() {
            animator.SetTrigger(Animator.StringToHash("Restart"));
        }
        
        private void ResetRestart() {
            animator.ResetTrigger(Animator.StringToHash("Restart"));
        }

        private void PlayDeathScreen() {
            animator.SetTrigger(Animator.StringToHash("DeathScreen"));
        }
        
    }
}