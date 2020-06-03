using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Controllers;
using RPG.Entities.Movement;
using RPG.Generators;
using UnityEngine;

public class Stairs : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("Player")) {
            LevelGenerator.LevelNumber++;
            GameController.Instance.RestartGame();
            LevelGenerator.GeneratingLevel = true;
        }
    }
}
