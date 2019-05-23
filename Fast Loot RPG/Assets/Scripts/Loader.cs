using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    [SerializeField] GameObject gameManagerPrefab;
    [SerializeField] GameObject playerPrefab;

    private void Awake()
    {
        if (GameManager.Instance == null)
            Instantiate(gameManagerPrefab);
        if (Player.Instance == null)
            Instantiate(playerPrefab);
    }
}
