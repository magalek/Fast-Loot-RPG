using System;
using RPG.Entities;
using UnityEngine;

namespace RPG.HitTriggers {
    public class WeaponHitTrigger : MonoBehaviour {

        [SerializeField] private GameObject sparksPrefab;
        
        private Animator animator;
        private static readonly int Interrupt = Animator.StringToHash("Interrupt");

        private void Awake() {
            animator = GetComponentInParent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D other) {
            IHittable hittable = other.GetComponentInParent<IHittable>();
            if (hittable != null) {
                hittable.Hit(Player.Instance.characterInfo.Damage.Current);
                hittable.IsHittable = false;
                // animator.SetTrigger(Interrupt);
            }
            // else {
            //     animator.speed = -0.5f;
            //     var position = Player.Instance.transform.position;
            //     var _direction = (transform.position - position );
            //     GameObject sparks = Instantiate(sparksPrefab, transform.position , Quaternion.identity);
            //     _direction = new Vector3(_direction.y, _direction.x, _direction.z); 
            //     sparks.transform.rotation = Quaternion.FromToRotation(position, _direction);
            //     animator.SetTrigger(Interrupt);
            // }
            // animator.speed = 1;
        }

        private void OnTriggerExit2D(Collider2D other) {
            IHittable hittable = other.GetComponentInParent<IHittable>();
            if (hittable != null) {
                hittable.IsHittable = true;
            }
        }
    }
} 