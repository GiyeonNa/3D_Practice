using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Gun,
    Boom,
    Knife
}

public class PlayerManager : MonoBehaviour, IDamageable
{
    public static PlayerManager Instance { get; private set; }

    [SerializeField]
    private PlayerStatsSO stats;

    [SerializeField]
    private List<WeaponSO> weaponList;

    public int CurrentHealth { get; private set; }
    public WeaponSO CurrentWeapon { get; private set; }

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
        SwapWeapon(weaponList[0]);
    }

    private void HandleWeaponSwap()
    {
        // Handle keyboard input
        if (Input.GetKeyDown(KeyCode.Alpha1) && weaponList.Count > 0)
        {
            Debug.Log("Gun Mode");
            SwapWeapon(weaponList[0]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && weaponList.Count > 1)
        {
            Debug.Log("Boom Mode");
            SwapWeapon(weaponList[1]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && weaponList.Count > 2)
        {
            Debug.Log("Knife Mode");
            SwapWeapon(weaponList[2]);
        }

        // Handle mouse wheel input
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f) // Scroll up
        {
            CycleWeapon(1);
        }
        else if (scroll < 0f) // Scroll down
        {
            CycleWeapon(-1);
        }
    }

    private void CycleWeapon(int direction)
    {
        if (weaponList == null || weaponList.Count == 0) return;

        int currentIndex = weaponList.IndexOf(CurrentWeapon);
        int newIndex = (currentIndex + direction + weaponList.Count) % weaponList.Count;
        SwapWeapon(weaponList[newIndex]);
    }

    private void SwapWeapon(WeaponSO weaponSO)
    {
        if (CurrentWeapon != null)
        {
            CurrentWeapon.currentAmmo = PlayerFire.Instance.GetCurrentAmmo(); // Save current ammo
        }

        CurrentWeapon = weaponSO; // Assign the new weapon
        CurrentWeapon.InitializeAmmo(); // Ensure ammo is initialized
        PlayerFire.Instance.UpdateAmmoCount(CurrentWeapon.currentAmmo, CurrentWeapon.MaxAmmo); // Update PlayerFire ammo count
        PlayerUI.Instance.UpdateWeaponUI(weaponSO); // Ensure UI is updated when weapon is swapped
    }

    public PlayerStatsSO GetInfo()
    {
        return stats;
    }

    public WeaponSO GetWeaponInfo()
    {
        return CurrentWeapon;
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
