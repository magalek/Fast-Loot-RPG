using RPG.Controllers;
using RPG.Entities;
using RPG.Generators;
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
            
            LevelGenerator.GenerationCompleted += () => Instantiate(ResourcesController.playerPrefab);
        }
    }
}
