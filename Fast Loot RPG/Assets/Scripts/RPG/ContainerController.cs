using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG {
    public class ContainerController : MonoBehaviour {
        public static ContainerController Instance;

        private Container containerOne;
        private Container containerTwo;
        
        private void Awake() {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }

        public void ChangeContainerState(Container container, bool state) {

            if (!state) {
                if (containerOne == container) {
                    containerOne = null;
                }
                if (containerTwo == container) {
                    containerTwo = null;
                }
            }
            else {
                if (containerOne == null) {
                    containerOne = container;
                }
                else if (containerTwo == null) {
                    containerTwo = container;
                }
            }

            if (containerOne != null) {
                containerOne.Target = containerTwo;
            }
            if (containerTwo != null) {
                containerTwo.Target = containerOne;
            }
        }
    }
}