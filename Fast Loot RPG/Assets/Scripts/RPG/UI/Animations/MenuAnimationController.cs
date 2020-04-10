using System;
using UnityEngine;

namespace RPG.UI.Animations {
    public class MenuAnimationController : MonoBehaviour {

        private Animator animator;

        private void Awake() {
            animator = GetComponent<Animator>();
        }

        public void PlayStartGame() {
            animator.SetTrigger(Animator.StringToHash("StartGame"));
        }
    }
}