using RPG.Entities;
using RPG.Managers;
using UnityEngine;

namespace RPG
{
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
}
