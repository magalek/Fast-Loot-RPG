using System;
using System.Collections;
using RPG.Controllers;
using RPG.Entities.Movement;
using UnityEngine;

namespace RPG.Utility {
    public class Cooldown<T> {

        private readonly T calee;

        public float CooldownTime { get; set; }
        private float elapsedTime;

        private bool isStarted;
        
        public event Action<T> Ended;
        public float Percentage => isStarted ? elapsedTime / (CooldownTime * 10) : 1;

        public Cooldown(T calee, float cooldownTime, params Action<T>[] callbacks) {
            this.calee = calee;
            CooldownTime = cooldownTime;
            isStarted = false;
            
            foreach (Action<T> callback in callbacks) {
                Ended += callback;
            }
        }

        public void Start() {
            isStarted = true;
            GameController.Instance.StartCoroutine(CooldownCoroutine());
        }

        private IEnumerator CooldownCoroutine() {

            for (float i = 0; i < CooldownTime * 10; i++) {
                yield return new WaitForSeconds(0.1f);
                elapsedTime = i;
            }

            elapsedTime++;
            Ended?.Invoke(calee);
        }
    }
}