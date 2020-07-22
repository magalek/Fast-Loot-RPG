using System;
using System.Collections;
using RPG.Controllers;
using RPG.Entities.Movement;
using UnityEngine;

namespace RPG.Utility {
    public class Cooldown<T> {

        private readonly T calee;
        
        private readonly float cooldownTime;
        private float elapsedTime;
        
        public event Action<T> Ended;
        public float Percentage => elapsedTime / (cooldownTime * 10);
        
        public Cooldown(T calee, float cooldownTime, params Action<T>[] callbacks) {
            this.calee = calee;
            this.cooldownTime = cooldownTime;
            foreach (Action<T> callback in callbacks) {
                Ended += callback;
            }
        }

        public void Start() {
            GameController.Instance.StartCoroutine(CooldownCoroutine());
        }

        private IEnumerator CooldownCoroutine() {

            for (float i = 0; i < cooldownTime * 10; i++) {
                yield return new WaitForSeconds(0.1f);
                elapsedTime = i;
            }

            elapsedTime++;
            Ended?.Invoke(calee);
        }
    }
}