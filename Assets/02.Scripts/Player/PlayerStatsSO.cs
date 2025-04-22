using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats", order = 1)]
public class PlayerStatsSO : ScriptableObject
{
    public float MoveSpeed = 7f;
    public float DashSpeed = 12f;
    public float JumpPower = 5f;
    public float MaxStamina = 100f;
    public float RollSpeed = 1f;
    public float ClimbStaminaCost = 20f; 
    public float DashStaminaCost = 20f; 
    public float RollStaminaCost = 10f; 
    public float StaminaRecoveryRate = 5f;
    public int MaxBullets = 50;
    public int MaxBombs = 3;
    public float ReloadTime = 2f;

}

