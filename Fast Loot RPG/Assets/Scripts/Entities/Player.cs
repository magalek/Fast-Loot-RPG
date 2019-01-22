using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : Entity {
    [SerializeField] TextMeshProUGUI hpText;

    private void Update()
    {
        hpText.text = "Your HP: " + healthPoints;
    }

}
