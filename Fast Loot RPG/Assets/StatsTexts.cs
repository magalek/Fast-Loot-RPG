using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Controllers;
using RPG.Entities;
using TMPro;
using UnityEngine;

public class StatsTexts : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI damageText;

    private void Start() {
        Player.Spawned += () => {
            Player.Instance.characterInfo.Damage.Changed += ChangeTexts;
            Player.Instance.characterInfo.Health.Changed += ChangeTexts;
            ChangeTexts();
        };
        
    }

    private void ChangeTexts() {
        healthText.text = $"{Player.Instance.characterInfo.Health.Current}/{Player.Instance.characterInfo.Health.Max}";
        damageText.text = $"{Player.Instance.characterInfo.Damage.Current}";
    }
}
