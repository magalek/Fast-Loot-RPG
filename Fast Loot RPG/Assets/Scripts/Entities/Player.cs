public class Player : Entity
{
    public static Player Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        effectManager.AddEffect(new Effect(statistics, 3, effectManager));
    }
}
