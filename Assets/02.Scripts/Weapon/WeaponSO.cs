using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon SO/Weapon")]
public class WeaponSO : ScriptableObject
{
    public WeaponType Type; // Matches the WeaponType enum in PlayerManager
    public int Damage; // Damage dealt by the weapon
    public float FireRate; // Rate of fire for the weapon
    public int currentAmmo; // Current ammo count
    public int MaxAmmo; // Maximum ammo capacity
    public Sprite Icon; 

    [Header("Additional Attributes")]
    public float ReloadTime; // Time required to reload the weapon
    public float Range; // Effective range of the weapon
    public float MaxThrowForce; // For throwable weapons like Boom

    public void InitializeAmmo()
    {
        if (currentAmmo == 0)
        {
            currentAmmo = MaxAmmo; // Initialize ammo to max if not already set
        }
    }
}
