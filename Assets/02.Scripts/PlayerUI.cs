using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI Instance { get; private set; }

    [SerializeField]
    private Image staminaBar;
    [SerializeField]
    private Image healthBar;
    [SerializeField]
    private TextMeshProUGUI bulletCountText;
    [SerializeField]
    private TextMeshProUGUI bombCountText;
    [SerializeField]
    private Image crosshairImage;
    [SerializeField]
    private Image reloadImage;
    [SerializeField]
    private Image throwForceImage;
    [SerializeField]
    private Image damagedImage;
    [SerializeField]
    private GameObject gameMaskObject;
    [SerializeField]
    private TextMeshProUGUI countdownText;

    public Button testButton;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        testButton.onClick.AddListener(OnTestButtonClick);
    }
    private void OnTestButtonClick()
    {
        Damage temp = new Damage();
        temp.Value = 10;
        PlayerManager.Instance.TakeDamage(temp);

    }

    public void SetStamina(float stamina, float maxStamina)
    {
        if (staminaBar != null)
            staminaBar.fillAmount = stamina / maxStamina;
    }

    public void SetHealth(float health, float maxHealth)
    {
        if (healthBar != null)
            healthBar.fillAmount = health / maxHealth;
    }

    public IEnumerator ReloadProgress(float reloadTime)
    {
        crosshairImage.gameObject.SetActive(false);
        reloadImage.gameObject.SetActive(true);
        float elapsedTime = 0f;
        while (elapsedTime < reloadTime)
        {
            reloadImage.fillAmount = 1f - (elapsedTime / reloadTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        reloadImage.fillAmount = 0f;
        reloadImage.gameObject.SetActive(false);
        crosshairImage.gameObject.SetActive(true);
    }

    public void SetDamageEffect(float duration = 0.2f)
    {
        if (damagedImage != null)
        {
            damagedImage.gameObject.SetActive(true);
            StartCoroutine(DamageEffectCoroutine(duration));
        }
    }

    private IEnumerator DamageEffectCoroutine(float duration)
    {
        float elapsedTime = 0f;
        Color originalColor = damagedImage.color;

        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(0.5f, 0f, elapsedTime / duration);
            damagedImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public void SetCountdownText(int seconds)
    {
        if (seconds == 0)
        {
            gameMaskObject.SetActive(false);
            return;
        }

        if (countdownText != null)
        {
            gameMaskObject.SetActive(true);
            string temp = $"Game Ready \n {(seconds > 0 ? seconds.ToString() : string.Empty)}";
            countdownText.text = temp;
        }
    }

    public void SetGameOver()
    {
        if (gameMaskObject != null)
        {
            gameMaskObject.SetActive(true);
            countdownText.text = "Game Over!";
        }
    }

    #region bullet
    public void SetBulletCount(int count, int maxcount)
    {
        if (bulletCountText != null)
            bulletCountText.text = count + "/" + maxcount;
    }
    #endregion

    #region bomb
    public void SetBombCount(int count, int maxcount)
    {
        if (bombCountText != null)
            bombCountText.text = count + "/" + maxcount;

        if (throwForceImage != null)
            throwForceImage.gameObject.SetActive(false);
    }
    public void UpdateThrowForceUI(float currentForce, float maxForce)
    {
        if (throwForceImage != null)
        {
            throwForceImage.gameObject.SetActive(true);
            throwForceImage.fillAmount = currentForce / maxForce;
        }
    }
    #endregion


    public void UpdateWeaponUI(WeaponType weaponType)
    {
        // Update the UI to reflect the currently selected weapon
        // For example, highlight the selected weapon icon
    }
}