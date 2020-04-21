using System;
using System.Collections;
using RPG.UI;
using UnityEngine;

namespace RPG.Entities {
    public class Weapon : MonoBehaviour {

        private float attackCooldownTime = 0.4f;
        private bool onCooldown;
        
        private Animator animator;

        private void Awake() {
            animator = GetComponent<Animator>();
        }
        
        private void Rotate() {
            Vector3 mouseWorldPosition = MainCamera.Instance.camera.ScreenToWorldPoint(Input.mousePosition);
            
            float angleRad = Mathf.Atan2(mouseWorldPosition.y - transform.position.y, mouseWorldPosition.x - transform.position.x);
            float angleDeg = (180 / Mathf.PI) * angleRad;

            transform.rotation = Quaternion.Euler(0, 0, angleDeg);
        }
        
        public void Attack() {
            if (onCooldown) return;
            onCooldown = true;
            StartCoroutine(WeaponCooldown());
            Rotate();
            if (Input.mousePosition.x < Screen.width / 2)
                animator.SetTrigger(Animator.StringToHash("Attack Left"));
            else
                animator.SetTrigger(Animator.StringToHash("Attack Right"));
        }

        private IEnumerator WeaponCooldown() {
            yield return new WaitForSeconds(attackCooldownTime);
            onCooldown = false;
        }
    }
}