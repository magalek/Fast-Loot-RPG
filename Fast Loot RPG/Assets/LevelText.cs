using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Generators;
using TMPro;
using UnityEngine;

public class LevelText : MonoBehaviour {

    private TextMeshProUGUI text;

    private void Awake() {
        text = GetComponent<TextMeshProUGUI>();

        LevelGenerator.GenerationCompleted += () => {
            text.text = $"Level {LevelGenerator.LevelNumber}";
            GetComponent<Animator>().SetTrigger("Fire");
        };
    }
}
