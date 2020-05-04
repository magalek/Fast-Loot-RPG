using System;
using UnityEngine;

namespace RPG.Statistics {
    [Serializable]
    public class Damage : IStatistic {
        public event Action Changed;

        [SerializeField] private int current;
        
        public int Current {
            get => current;
            set => current = value;
        }

        public void Init() {
        }

        public void Add(int amount) {
            throw new NotImplementedException();
        }

        public void Subtract(int amount) {
            throw new NotImplementedException();
        }
    }
}