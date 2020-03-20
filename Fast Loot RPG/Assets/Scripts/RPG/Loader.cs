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
            // if (Player.Instance == null) 
            //     Instantiate(ResourcesController.playerPrefab);
        }
    }
}
