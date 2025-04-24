using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    [SerializeField]
    private PlayerStatsSO stats;

    public int CurrentHealth { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializePlayerStats();
    }

    private void InitializePlayerStats()
    {
        CurrentHealth = stats.MaxHealth;
    }

    public PlayerStatsSO GetInfo()
    {
        return stats;
    }

    public void ReduceHealth(int amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Debug.Log("Player is dead.");
        }
    }
}
