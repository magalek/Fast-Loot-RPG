using RPG.Controllers;
using RPG.Entities;
using RPG.Generators;
using RPG.UI;
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
            
            GameObject containerController = new GameObject("Container Controller");
            containerController.AddComponent<ContainerController>();
            
            LevelGenerator.GenerationCompleted += () => {
                Instantiate(ResourcesController.playerPrefab);
                MainCamera.Instance.CenterOn(Player.Instance.transform, 1);
            };
        }
    }
}
