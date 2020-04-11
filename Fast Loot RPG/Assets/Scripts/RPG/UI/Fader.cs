using System;
using System.Collections;
using RPG.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI {
    public class Fader : MonoBehaviour {
        
        private static Animator animator;
        private void Awake() {
            animator = GetComponent<Animator>();
            Player.Spawned += FadeIn;
            Player.Died += FadeOut;
        }

        private static void FadeIn() {
            animator.SetTrigger(Animator.StringToHash("FadeIn"));
        }
        
        private static void FadeOut() {
            animator.SetTrigger(Animator.StringToHash("FadeOut"));
        }
    }
}