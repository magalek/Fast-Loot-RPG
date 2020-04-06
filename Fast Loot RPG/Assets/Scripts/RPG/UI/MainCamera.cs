using System;
using RPG.Entities;
using UnityEngine;

namespace RPG.UI {
    public class MainCamera : MonoBehaviour {

        public static MainCamera Instance;
        public Camera camera;
        
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);

            camera = GetComponent<Camera>();
            
            DontDestroyOnLoad(this);
        }

        private void Update() {
            if (Player.Instance == null) return;
            if (!Player.Instance.playerController.isMoving) {
                Center(Player.Instance.transform);
            }
        }

        public void Center(Transform transform) {
            this.transform.position = Vector2.Lerp(this.transform.position, new Vector3(transform.position.x, transform.position.y, -10), 0.1f);
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10);
        }
    }
}
