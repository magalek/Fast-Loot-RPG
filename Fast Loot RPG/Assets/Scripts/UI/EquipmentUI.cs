using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUI : MonoBehaviour
{
    [SerializeField] Canvas equipmentCanvasRef;

    private void Start()
    {
        equipmentCanvasRef.worldCamera = MainCamera.Instance.GetComponent<Camera>();
    }
}
