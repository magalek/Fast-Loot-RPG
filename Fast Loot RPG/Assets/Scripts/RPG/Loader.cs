using RPG.Controllers;
using RPG.Entities;
using UnityEngine;

namespace RPG
{
    public class Loader : MonoBehaviour
    {
        private void Awake()
        {
            ResourcesController.Initialise();
            if (GameController.Instance == null)
                Instantiate(ResourcesController.gameControllerPrefab);
            
            LevelController.GenerationCompleted += () => Instantiate(ResourcesController.playerPrefab);
        }
    }
}
