using System;
using UnityEngine;

namespace RPG.Statistics {
    [Serializable]
    public class Health : IMaxStatistic<int> {
        public event Action Changed;

        [SerializeField] private int current;
        [SerializeField] private int max;

        public int Max {
            get => max;
            set => max = value;
        }

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
            Current = max;
        }

        public void ChangeMaxBy(int amount) {
            max += amount;
        }
        
        public void ChangeCurrentBy(int amount) {
            Current += amount;
        }
    }
}