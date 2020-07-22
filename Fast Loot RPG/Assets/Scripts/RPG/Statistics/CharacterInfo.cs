using System;
using UnityEngine;

namespace RPG.Statistics {
    public class CharacterInfo : MonoBehaviour{
        public Damage Damage;
        public Health Health;

        private void Awake() {
            Init();
        }

        public void Init() {
            Damage.Init();
            Health.Init();
        }
    }
}