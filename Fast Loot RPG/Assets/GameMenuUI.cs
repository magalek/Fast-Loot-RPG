using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuUI : MonoBehaviour
{
    private void Awake() {
        foreach (Transform child in transform) {
            child.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            foreach (Transform child in transform) {
                child.gameObject.SetActive(!child.gameObject.activeSelf);
            }
        }
    }
}
