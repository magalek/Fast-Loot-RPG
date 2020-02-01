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
    }
}
