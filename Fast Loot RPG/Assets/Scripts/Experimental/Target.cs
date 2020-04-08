using UnityEngine;

namespace Experimental {
    public class Target : MonoBehaviour {
        public static Target Instance;
        
        // Start is called before the first frame update
        void Awake() {
            Instance = this;
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
