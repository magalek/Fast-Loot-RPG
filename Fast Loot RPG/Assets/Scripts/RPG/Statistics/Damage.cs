using System;
using UnityEngine;

namespace RPG.Statistics {
    [Serializable]
    public class Damage : IStatistic<int> {
        public event Action Changed;

        [SerializeField] private int current;
        
        public int Current {
            get => current;
            set {
                current = value;
                Changed?.Invoke();
            }
        }

        public void Init() {
        }
        
        public void ChangeCurrentBy(int amount) {
            Current += amount;
        }
    }
}