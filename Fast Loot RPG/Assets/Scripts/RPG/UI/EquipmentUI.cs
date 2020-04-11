using System;
using UnityEngine;

namespace RPG.UI {
    public class EquipmentUI : MonoBehaviour
    {
        [SerializeField] Canvas equipmentCanvas;

        private void Start()
        {
            equipmentCanvas.worldCamera = MainCamera.Instance.GetComponent<Camera>();
            //gameObject.SetActive(false);
        }
    }
}
