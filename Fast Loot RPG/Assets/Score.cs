using RPG.Entities;
using RPG.Generators;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private TextMeshProUGUI endGameScreenText;
    public int Amount {
        get => amount;
        set {
            amount = value;
            textComponent.text = Amount.ToString();
            endGameScreenText.text = $"Score: {Amount}";
        } 
    }

    public static Score Instance;

    private int amount;
    
    private void Awake() {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        LevelGenerator.LevelRestarted += () => Amount = 0;
    }

    private void Start() {
        textComponent = GetComponent<TextMeshProUGUI>();
        endGameScreenText = GameObject.Find("Middle Text").GetComponent<TextMeshProUGUI>();
    }
}
