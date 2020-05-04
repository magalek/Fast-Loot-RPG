using System;
using UnityEngine;

namespace RPG.Statistics {
    [Serializable]
    public class Health : IStatistic {
        public event Action Changed;

        [SerializeField] private int current;
        [SerializeField] private int max;

        public int Max => max;
        public int Current {
            get => current;
            set {
                if (value > max) {
                    current = max;
                }
                current = value;
                Changed?.Invoke();
            }
        }
        
        public float Percentage => (float) Current / max;
        public bool ZeroOrLess => Current <= 0;

        public void Init() {
            current = max;
        }

        public void Add(int amount) {
            Current += amount;
        }
        
        public void Subtract(int amount) {
            Current -= amount;
        }
    }
}