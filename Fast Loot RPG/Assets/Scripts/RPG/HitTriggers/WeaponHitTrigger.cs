using System;
using RPG.Entities;
using UnityEngine;

namespace RPG.HitTriggers {
    public class WeaponHitTrigger : MonoBehaviour {
        private void OnTriggerEnter2D(Collider2D other) {
            if (other.TryGetComponent<IHittable>(out IHittable hittable))
                hittable.Hit(10);
            
        }
    }
} 