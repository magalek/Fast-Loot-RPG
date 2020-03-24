using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace RPG.Lighting {
    public class LightGlow : MonoBehaviour {
        private Light2D Light2D => GetComponent<Light2D>();

        private void Awake() {
            StartCoroutine(Glow());
        }

        private IEnumerator Glow() {
            float amount = 0;
            while (true) {
                if (Light2D.intensity > 0.9f) amount = -0.001f;
                if (Light2D.intensity < 0.7f) amount = 0.001f;

                Light2D.intensity += amount;
                //yield return new WaitForSeconds(0.01f);
                yield return null;
            }
        }
    }
}