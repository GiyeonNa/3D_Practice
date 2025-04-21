using UnityEngine;

[System.Serializable]
public class PlayerStats 
{
    public float MoveSpeed = 7f;
    public float JumpPower = 5f;
    public float Stamina = 100f;
    public float MaxStamina = 100f;
    public float RollSpeed = 1f;

    public void AdjustStamina(float amount)
    {
        Stamina = Mathf.Clamp(Stamina + amount, 0, MaxStamina);
    }
}
