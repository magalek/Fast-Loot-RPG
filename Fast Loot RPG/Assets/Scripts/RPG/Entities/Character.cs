using System;
using UnityEngine;

namespace RPG.Entities
{
    [RequireComponent(typeof(Health))]
    public class Character : MonoBehaviour {

        public string entityName;

        public Health health;

        public void Awake() {
            health = GetComponent<Health>();
        }

        public virtual void Kill() {
            Destroy(gameObject);            
        }
    }
}
