using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI Instance { get; private set; }

    [SerializeField]
    private Image staminaBar;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void SetStamina(float stamina, float maxStamina)
    {
        if (staminaBar != null)
            staminaBar.fillAmount = stamina / maxStamina;
    }
}
