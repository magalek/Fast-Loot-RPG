using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUI : MonoBehaviour
{
    [SerializeField] Canvas equipmentCanvas;

    private void Start()
    {
        equipmentCanvas.worldCamera = MainCamera.Instance.GetComponent<Camera>();
    }
}
