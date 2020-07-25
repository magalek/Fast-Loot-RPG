using System;
using RPG.Entities;
using UnityEngine;

namespace RPG.HitTriggers {
    public class WeaponHitTrigger : MonoBehaviour {
        private void OnTriggerEnter2D(Collider2D other) {
            IHittable hittable = other.GetComponentInParent<IHittable>();
            if (hittable != null) {
                hittable.Hit(Player.Instance.characterInfo.Damage.Current);
                hittable.IsHittable = false;
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            IHittable hittable = other.GetComponentInParent<IHittable>();
            if (hittable != null) {
                hittable.IsHittable = true;
            }
        }
    }
} 