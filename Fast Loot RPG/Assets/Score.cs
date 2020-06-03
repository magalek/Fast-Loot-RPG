using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour {
    private static TextMeshProUGUI textComponent;
    private static int amount;

    public static int Amount {
        get => amount;
        set {
            amount = value;
            textComponent.text = $"Score: {amount}";
        }
    }

    private void Awake() {
        textComponent = GetComponent<TextMeshProUGUI>();
    }
}
