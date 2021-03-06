﻿using System;
using System.Collections;
using RPG.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI {
    public class Fader : MonoBehaviour {
        
        private static Animator animator;
        private void Awake() {
            animator = GetComponent<Animator>();
            Player.Created += FadeIn;
            Player.Spawned += FadeIn;
            Player.Died += FadeOut;
        }

        public static void FadeIn() {
            animator.SetTrigger(Animator.StringToHash("FadeIn"));
        }
        
        public static void FadeOut() {
            animator.SetTrigger(Animator.StringToHash("FadeOut"));
        }
    }
}