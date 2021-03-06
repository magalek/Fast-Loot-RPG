﻿using System;
using System.Collections;
using RPG.UI;
using RPG.Utility;
using UnityEngine;

namespace RPG.Entities {
    public class Weapon : MonoBehaviour {

        private Cooldown<Weapon> attackCooldown;
        public bool CanAttack { get; set; } = true;

        private Animator animator;

        private void Awake() {
            animator = GetComponent<Animator>();
            attackCooldown = new Cooldown<Weapon>(this, 0.4f, (r) => r.CanAttack = true);
        }
        
        private void Rotate() {
            Vector3 mouseWorldPosition = MainCamera.Instance.camera.ScreenToWorldPoint(Input.mousePosition);
            
            float angleRad = Mathf.Atan2(mouseWorldPosition.y - transform.position.y, mouseWorldPosition.x - transform.position.x);
            float angleDeg = (180 / Mathf.PI) * angleRad;

            transform.rotation = Quaternion.Euler(0, 0, angleDeg);
        }
        
        public void Attack() {
            if (!CanAttack) return;
            CanAttack = false;
            attackCooldown.Start();
            Rotate();
            if (Input.mousePosition.x < Screen.width / 2)
                animator.SetTrigger(Animator.StringToHash("Attack Left"));
            else
                animator.SetTrigger(Animator.StringToHash("Attack Right"));
        }

    }
}