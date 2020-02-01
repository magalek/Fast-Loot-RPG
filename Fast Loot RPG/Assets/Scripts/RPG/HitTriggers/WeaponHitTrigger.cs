using System;
using RPG.Entities;
using UnityEngine;

namespace RPG.HitTriggers {
    public class WeaponHitTrigger : MonoBehaviour {
        
        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Enemy")) {
                other.GetComponent<Entity>().SubtractHealth(10);
            }
        }
    }
}