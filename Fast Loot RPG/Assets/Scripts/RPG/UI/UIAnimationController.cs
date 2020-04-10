using System;
using RPG.Entities;
using UnityEngine;

namespace RPG.UI {
    public class UIAnimationController : MonoBehaviour {

        private Animator animator;

        private void Awake() {
            animator = GetComponent<Animator>();
            Player.Died += PlayDeathScreen;
        }

        private void PlayDeathScreen() {
            animator.SetTrigger(Animator.StringToHash("DeathScreen"));
        }
    }
}