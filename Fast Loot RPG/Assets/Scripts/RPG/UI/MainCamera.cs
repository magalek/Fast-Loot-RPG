using System;
using System.Collections;
using RPG.Entities;
using UnityEngine;

namespace RPG.UI {
    public class MainCamera : MonoBehaviour {

        public static MainCamera Instance;
        public Camera camera;

        private bool centeringCamera;
        
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);

            camera = GetComponent<Camera>();
            
            DontDestroyOnLoad(this);
            centeringCamera = false;
        }

        private void Update() {
            if (Player.Instance == null) return;
            if (!centeringCamera && !Player.Instance.playerController.isMoving) {
                StartCoroutine(CenterCoroutine());
            }
        }

        public void Center(Transform transform, float speed) {
            this.transform.position = Vector2.Lerp(this.transform.position, new Vector3(transform.position.x, transform.position.y, -10), speed);
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10);
        }

        private IEnumerator CenterCoroutine() {
            while (!Player.Instance.playerController.isMoving) {
                centeringCamera = true;
                Center(Player.Instance.transform, 0.05f);
                yield return new WaitForSeconds(0.01f);
            }

            centeringCamera = false;
        }
    }
}
