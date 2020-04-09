using System;
using System.Collections;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI {
    public class PanelFader : MonoBehaviour {

        private Image image;
        private Camera camera;
        private void Awake() {
            camera = MainCamera.Instance.camera;
            image = GetComponent<Image>();
            StartCoroutine(FadeCoroutine());
        }

        private IEnumerator FadeCoroutine() {
            while (image.color.a > 0) {
                var color = image.color;
                color = new Color(color.r, color.g, color.b, color.a - Time.deltaTime * 0.5f);
                image.color = color;
                // yield return new WaitForSeconds(0.01f);
                yield return null;
            }
            yield return null;
        }
    }
}