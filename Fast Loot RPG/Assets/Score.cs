using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering.RenderGraphModule;

public class Score : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private TextMeshProUGUI endGameScreenText;
    public int Amount { get; private set; }

    public static Score Instance;

    private void Awake() {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    private void Start() {
        textComponent = GetComponent<TextMeshProUGUI>();
        endGameScreenText = GameObject.Find("Middle Text").GetComponent<TextMeshProUGUI>();
    }

    public void AddScore(int amount) {
        Amount += amount;
        textComponent.text = Amount.ToString();
        endGameScreenText.text = $"Score: {Amount}";
    }
}
