﻿using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Entities;
using RPG.UI;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour {
    private RPG.Statistics.CharacterInfo info;
    [SerializeField] private Image healthImage;
    [SerializeField] private Image missingHealthImage;
    [SerializeField] private Image borderImage;
    
    private void Awake() {
        Canvas canvas = GetComponentInParent<Canvas>();
        canvas.worldCamera = MainCamera.Instance.camera;
        canvas.sortingOrder = 19;
    }

    // Start is called before the first frame update
    void Start() {
        healthImage.color = Color.clear;
        missingHealthImage.color = Color.clear;
        borderImage.color = Color.clear;
        
        info = GetComponentInParent<RPG.Statistics.CharacterInfo>();
        info.Health.Changed += ChangeHealthBar;
    }

    private void ChangeHealthBar() {
        healthImage.color = Color.green;
        missingHealthImage.color = Color.red;
        borderImage.color = Color.black;

        healthImage.fillAmount = info.Health.Percentage;
    }
}
