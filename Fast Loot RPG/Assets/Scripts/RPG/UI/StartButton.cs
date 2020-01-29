using RPG.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class StartButton : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(GameManager.Instance.LoadLocation);
        }
    }
}
