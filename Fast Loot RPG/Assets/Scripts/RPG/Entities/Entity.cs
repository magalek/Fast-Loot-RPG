using System;
using UnityEngine;

namespace RPG.Entities
{
    [RequireComponent(typeof(Health))]
    public class Entity : MonoBehaviour {

        public string entityName;

        public Health health;
        public Stats stats;

        public void Awake() {
            health = GetComponent<Health>();
        }

        protected virtual void Kill() {
            Destroy(gameObject);            
        }
    }
}
