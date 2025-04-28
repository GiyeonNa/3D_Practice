using UnityEngine;

public enum WeaponType
{
    Gun,
    Knife
}

public class PlayerManager : MonoBehaviour, IDamageable
{
    public static PlayerManager Instance { get; private set; }

    [SerializeField]
    private PlayerStatsSO stats;

    public int CurrentHealth { get; private set; }
    public WeaponType CurrentWeapon { get; private set; }

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
        PlayerUI.Instance.SetHealth(CurrentHealth, stats.MaxHealth);
    }

    private void Update()
    {
        HandleWeaponSwap();
    }

    private void InitializePlayerStats()
    {
        CurrentHealth = stats.MaxHealth;
    }

    private void HandleWeaponSwap()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwapWeapon(WeaponType.Gun);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwapWeapon(WeaponType.Knife);
        }
    }

    private void SwapWeapon(WeaponType weaponType)
    {
        CurrentWeapon = weaponType;
        PlayerUI.Instance.UpdateWeaponUI(weaponType);
    }

    public PlayerStatsSO GetInfo()
    {
        return stats;
    }

    public void TakeDamage(Damage damage)
    {
        CurrentHealth -= damage.Value;
        PlayerUI.Instance.SetHealth(CurrentHealth, stats.MaxHealth);

        if(CurrentHealth <= 50)
        {
            PlayerMove.Instance.SetInjuredState(true);
        }

        if (CurrentHealth <= 0)
        {
            GameManager.Instance.GameOver();
            Debug.Log("Player is dead");
        }
    }
}
