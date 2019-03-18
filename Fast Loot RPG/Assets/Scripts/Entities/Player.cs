using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : Entity {
    public static Player Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(this);

        //PlayerEventHandler.PlayerHit += statistics.Checkstats;
    }
}
