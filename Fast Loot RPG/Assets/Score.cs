using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private int amount;

    private void Start() {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    public void AddScore(int amount) {
        this.amount += amount;
        textComponent.text = this.amount.ToString();
    }
}
