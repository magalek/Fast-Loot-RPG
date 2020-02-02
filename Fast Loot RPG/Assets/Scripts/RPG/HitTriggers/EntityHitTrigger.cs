using System;
using RPG.Entities;
using UnityEngine;

namespace RPG.HitTriggers {
    public class EntityHitTrigger : MonoBehaviour {
        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                other.GetComponent<Entity>().SubtractHealth(10);
            }
        }
    }
}