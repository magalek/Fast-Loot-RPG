using System;
using UnityEngine;

namespace RPG.Statistics {
    public class Info : MonoBehaviour{
        public Damage Damage;
        public Health Health;

        private void Awake() {
            Damage.Init();
            Health.Init();
        }
    }
}