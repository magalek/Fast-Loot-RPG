using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : Entity {

    [HideInInspector] public Inventory inventory;
    [SerializeField] TextMeshProUGUI killCountText;
    [SerializeField] TextMeshProUGUI hpText;

    public static int killCount;

    private void Start()
    {
        inventory = GetComponent<Inventory>();
    }

    private void Update()
    {
        killCountText.text = "Kill count: " + killCount;
        hpText.text = "Your HP: " + healthPoints;
    }

}
