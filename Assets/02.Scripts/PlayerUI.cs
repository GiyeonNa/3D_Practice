using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using Unity.VisualScripting;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI Instance { get; private set; }

    [SerializeField]
    private Image staminaBar;
    [SerializeField]
    private Image healthBar;
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
    [SerializeField]
    private Image zoomImage;
    [SerializeField]
    private int zoomIn = 15;
    [SerializeField]
    private int zoomOut = 60;

    [SerializeField]
    private Image weaponUIImage;
    [SerializeField]
    private TextMeshProUGUI weaponAmmoCount;

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

    public void ZoomImage(bool isZoomMode)
    {
        if (zoomImage != null)
        {
            Camera.main.fieldOfView = isZoomMode ? zoomIn : zoomOut;
            crosshairImage.gameObject.SetActive(!isZoomMode);
            zoomImage.gameObject.SetActive(isZoomMode);
        }
    }



    public void SetAmmoCount(int count, int maxcount)
    {
        if (weaponAmmoCount != null)
        {
            if(maxcount == 0)
                weaponAmmoCount.text = "-";
            else
                weaponAmmoCount.text = count + "/" + maxcount;
        }
    }


    public void UpdateThrowForceUI(float currentForce, float maxForce)
    {
        if (throwForceImage != null)
        {
            throwForceImage.gameObject.SetActive(true);
            throwForceImage.fillAmount = currentForce / maxForce;
        }
    }

    public void SetFalseThrowForceUI()
    {
        if (throwForceImage != null)
            throwForceImage.gameObject.SetActive(false);
    }


    public void UpdateWeaponUI(WeaponSO weapon)
    {
        if (weaponUIImage != null)
            weaponUIImage.sprite = weapon.Icon;

        if (weaponAmmoCount != null)
        {
            if (weapon.Type == WeaponType.Knife)
                SetAmmoCount(weapon.currentAmmo, 0);
            else
                SetAmmoCount(weapon.currentAmmo, weapon.MaxAmmo); // Update ammo count
        }
    }
}