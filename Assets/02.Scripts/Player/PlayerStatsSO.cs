using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats", order = 1)]
public class PlayerStatsSO : ScriptableObject
{
    [Header("Player Stats")]
    #region Stats
    public int MaxHealth;
    public float MoveSpeed;
    public float DashSpeed;
    public float JumpPower;
    public float MaxStamina;
    public float RollSpeed;
    public float ClimbStaminaCost;
    public float DashStaminaCost;
    public float RollStaminaCost;
    public float StaminaRecoveryRate;
    #endregion

    [Header("Combat")]
    #region Combat
    public int MaxBullets;
    public int MaxBombs;
    public float ReloadTime;
    public float MaxThrowForce;
    public float FireRate;
    #endregion

}

