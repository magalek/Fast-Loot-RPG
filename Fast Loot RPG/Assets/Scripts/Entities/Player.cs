﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : Entity {
    public static Player Instance;

    [SerializeField] TextMeshProUGUI hpText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        DontDestroyOnLoad(this);

        hpText = GameObject.Find("Player HP")?.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (hpText != null)
            hpText.text = "Your HP: " + healthPoints;
    }

}