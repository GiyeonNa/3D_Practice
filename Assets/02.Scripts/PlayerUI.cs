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
    private TextMeshProUGUI bulletCountText;
    [SerializeField]
    private TextMeshProUGUI bombCountText;
    [SerializeField]
    private Image crosshairImage;
    [SerializeField]
    private Image reloadImage;


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
    }
    #endregion
}