﻿using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Random = UnityEngine.Random;

namespace RPG.Lighting {
    public class LightGlow : MonoBehaviour {
        private Light2D Light2D;

        [SerializeField] private Vector2 intensityPower; 
        
        private void Awake() {
            Light2D = GetComponent<Light2D>();
            Light2D.intensity = Random.value;
            StartCoroutine(Glow());
        }

        private IEnumerator Glow() {
            float intensity = 0.001f;
            while (true) {
                if (Light2D.intensity > intensityPower.y) intensity = -0.001f;
                if (Light2D.intensity < intensityPower.x) intensity = 0.001f;

                Light2D.intensity += intensity;
                //yield return new WaitForSeconds(0.01f);
                yield return null;
            }
        }
    }
}