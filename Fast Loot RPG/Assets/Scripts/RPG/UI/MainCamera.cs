using System.Collections;
using RPG.Entities;
using RPG.Entities.Movement;
using UnityEngine;

namespace RPG.UI {
    public class MainCamera : MonoBehaviour {

        public static MainCamera Instance;
        public Camera camera;

        public bool isCentering = false;

        private IMoveable target;
        private Transform targetTransform;
        private float centeringSpeed;
        private float zoomPower;
        
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
            if (target == null) return;
            if (!isCentering && !target.IsMoving) {
                StartCoroutine(CenterCoroutine());
            }
        }

        public void CenterOn(Transform target, float centeringSpeed, float zoomPower = 1) {
            targetTransform = target;
            this.target = targetTransform.GetComponent<IMoveable>();
            this.centeringSpeed = centeringSpeed;
            this.zoomPower = zoomPower;
        }
        
        public void CenterManually(Transform transform, float centeringSpeed) {
            this.transform.position = Vector2.Lerp(this.transform.position, new Vector3(transform.position.x, transform.position.y, -10), centeringSpeed);
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, zoomPower, centeringSpeed);
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10);
            
        }

        private IEnumerator CenterCoroutine() {
            while (target != null && !target.IsMoving) {
                isCentering = true;
                CenterManually(targetTransform, centeringSpeed);
                yield return new WaitForSeconds(0.01f);
            }

            isCentering = false;
        }
    }
}
