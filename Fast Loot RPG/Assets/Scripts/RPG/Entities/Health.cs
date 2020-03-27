using System;
using UnityEngine;

namespace RPG.Entities {
    public class Health : MonoBehaviour {
        public event Action Changed;
        
        public int current;
        public int max;
        public float percentage => (float) current / max;
        public bool zeroOrLess => current <= 0;

        private void Awake() {
            Reset();
        }

        public void Add(int amount) {
            current += amount;
            
            if (current > max) 
                Reset();
            else
                Changed?.Invoke();
        }
        
        public void Subtract(int amount) {
            current -= amount;
            
            Changed?.Invoke();
        }

        public void Reset() {
            current = max;
            
            Changed?.Invoke();
        }
    }
}