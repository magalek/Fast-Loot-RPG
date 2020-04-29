using System;
using System.Collections;
using RPG.Controllers;
using UnityEngine;

namespace RPG.Utility {
    public class Cooldown {

        public Action Started;
        public Action Ended;
        
        private float cooldownTime;
        
        public Cooldown(float cooldownTime) {
            this.cooldownTime = cooldownTime;
        }

        public void Start() {
            GameController.Instance.StartCoroutine(CooldownCoroutine());
        }

        private IEnumerator CooldownCoroutine() {
            Started?.Invoke();
            yield return new WaitForSeconds(cooldownTime);
            Ended?.Invoke();
        }
    }
}