using System;
using System.Runtime.CompilerServices;
using RPG.Entities;
using RPG.Utility;
using UnityEngine;

namespace RPG.HitTriggers {
    public class EntityHitTrigger : MonoBehaviour {
        
        [SerializeField] private Cooldown<EntityHitTrigger> cooldown;

        private bool canHit = true;
        
        private void Awake() {
            cooldown = new Cooldown<EntityHitTrigger>(this, 1, e => e.canHit = true);
        }

        private void OnTriggerStay2D(Collider2D other) {
            if (canHit && other.CompareTag("Player")) {
                other.GetComponentInParent<IHittable>().Hit(10);
                canHit = false;
                cooldown.Start();
            }
        }
    }
}