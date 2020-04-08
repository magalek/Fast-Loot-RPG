using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI {
    public class PanelFader : MonoBehaviour {

        private Image image;
        private void Awake() {
            image = GetComponent<Image>();
            StartCoroutine(FadeCoroutine());
        }

        private IEnumerator FadeCoroutine() {
            while (image.color.a > 0) {
                var color = image.color;
                color = new Color(color.r, color.g, color.b, color.a - 0.02f);
                image.color = color;
                yield return new WaitForSeconds(0.01f);
            }
            yield return null;
        }
    }
}