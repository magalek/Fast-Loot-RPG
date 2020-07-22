using System;
using RPG.Entities;
using UnityEngine;

namespace RPG.HitTriggers {
    public class WeaponHitTrigger : MonoBehaviour {
        private void OnTriggerEnter2D(Collider2D other) {
            if (other.TryGetComponent(out IHittable hittable)) {
                hittable.Hit(Player.Instance.characterInfo.Damage.Current);
                hittable.IsHittable = false;
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (other.TryGetComponent(out IHittable hittable)) {
                hittable.IsHittable = true;
            }
        }
    }
} 