using System;
using RPG.Entities.AnimationControllers;
using UnityEngine;

namespace RPG.Entities
{
    public class Entity : MonoBehaviour {

        public string entityName;
        public event Action HealthChanged;
        public Statistics statistics; 
        public float healthPercentage => (float)statistics.healthPoints / statistics.maxHealthPoints;

        public EntityAnimationController animationController;
        
        public virtual void AddHealth(int amount) {
            var currentHealth = statistics.healthPoints += amount;
            if (currentHealth > statistics.maxHealthPoints) 
                statistics.healthPoints = statistics.maxHealthPoints;
            HealthChanged?.Invoke();
        }

        public virtual void SubtractHealth(int amount) {
            if ((statistics.healthPoints -= amount) <= 0)
                Kill();
            else {
                animationController.PlayHit();
                statistics.healthPoints -= amount;
                HealthChanged?.Invoke();
            }
        }

        protected virtual void Kill() {
            //abilitiesController.DetachEvents();
            Destroy(gameObject);
        }

        protected virtual void SetTag() {
        }

        public virtual void SetComponents() {
            animationController = GetComponent<EntityAnimationController>();
        }
    }
}
