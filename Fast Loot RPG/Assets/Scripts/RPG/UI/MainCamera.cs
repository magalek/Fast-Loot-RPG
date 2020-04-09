using System;
using System.Collections;
using RPG.Entities;
using UnityEngine;

namespace RPG.UI {
    public class MainCamera : MonoBehaviour {

        public static MainCamera Instance;
        public Camera camera;

        public bool isCentering = false;
        public bool isZooming;
        
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);

            camera = GetComponent<Camera>();
            
            DontDestroyOnLoad(this);
            Debug.Log(camera.orthographicSize);
        }

        private void Update() {
            if (Player.Instance == null) return;
            if (!isCentering && !Player.Instance.playerController.isMoving) {
                StartCoroutine(CenterCoroutine());
            }
        }

        public void Center(Transform transform, float speed) {
            this.transform.position = Vector2.Lerp(this.transform.position, new Vector3(transform.position.x, transform.position.y, -10), speed);
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10);
        }

        private void Zoom(float timeFactor) {
            camera.orthographicSize += (Mathf.Exp(timeFactor) / 1000) + 0.01f;
        }

        private IEnumerator CenterCoroutine() {
            while (!Player.Instance.playerController.isMoving) {
                isCentering = true;
                Center(Player.Instance.transform, 0.05f);
                yield return new WaitForSeconds(0.01f);
            }

            isCentering = false;
        }

        public IEnumerator ZoomCoroutine() {
            float time = 1;
            while (camera.orthographicSize < 1) {
                isZooming = true;
                if (camera.orthographicSize > 0.9f)
                    time -= Time.deltaTime * 5;

                Zoom(time);
                //yield return new WaitForSeconds(0.1f);
                yield return null;
            }

            isZooming = false;
        }
    }
}
