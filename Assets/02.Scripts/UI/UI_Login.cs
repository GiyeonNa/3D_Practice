using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Security.Cryptography;
using System.Text;
using DG.Tweening;
using UnityEngine.SceneManagement;

[Serializable]
public class InputField
{
    public TMP_InputField UsernameInput;
    public TMP_InputField PasswordInput;
    public TMP_InputField PasswordCheckInput;
}

public class UI_Login : MonoBehaviour
{
    [SerializeField]
    private GameObject loginPanel;
    [SerializeField]
    private GameObject registerPanel;

    [Header("Login Fields")]
    [SerializeField]
    private Button registerButton;
    [SerializeField]
    private Button loginButton;
    [SerializeField]
    private InputField loginInputField;
    [SerializeField]
    private TextMeshProUGUI loginResultText;

    [Header("Register Fields")]
    [SerializeField]
    private InputField registerInputField;
    [SerializeField]
    private Button signUpButton;
    [SerializeField]
    private Button backButton;
    [SerializeField]
    private TextMeshProUGUI registerResultText;


    private const string PREFIX = "PlayerPrefs_";

    private void Awake()
    {
        registerButton.onClick.AddListener(OnClickRegister);
        loginButton.onClick.AddListener(OnClickLogin);
        signUpButton.onClick.AddListener(OnClickSignUp);
        backButton.onClick.AddListener(OnClickBackToLogin);

        loginInputField.UsernameInput.onValueChanged.AddListener(delegate { LoginCheck(); });
        loginInputField.PasswordInput.onValueChanged.AddListener(delegate { LoginCheck(); });
    }

    private void Start()
    {
        loginPanel.SetActive(true);
        registerPanel.SetActive(false);
        LoginCheck();
    }

    //ȸ������ â���� �̵�
    private void OnClickRegister()
    {
        loginPanel.SetActive(false);
        registerPanel.SetActive(true);

        registerResultText.text = string.Empty; 
        registerResultText.gameObject.SetActive(false); 

        registerInputField.UsernameInput.text = string.Empty; 
        registerInputField.PasswordInput.text = string.Empty;
        registerInputField.PasswordCheckInput.text = string.Empty;
    }

    //�α��� â���� �̵�
    private void OnClickBackToLogin()
    {
        loginPanel.SetActive(true);
        registerPanel.SetActive(false);

        loginResultText.text = string.Empty; 
        loginResultText.gameObject.SetActive(false); 

        loginInputField.UsernameInput.text = string.Empty; 
        loginInputField.PasswordInput.text = string.Empty;
    }

    //�α��� �õ�
    private void OnClickLogin()
    {
        if(string.IsNullOrEmpty(loginInputField.UsernameInput.text))
        {
            loginResultText.text = "���̵� �Է��ϼ���.";
            loginResultText.gameObject.SetActive(true);
            ShakeText(loginResultText);
            return;
        }

        if (string.IsNullOrEmpty(loginInputField.PasswordInput.text))
        {
            loginResultText.text = "��й�ȣ�� �Է��ϼ���.";
            loginResultText.gameObject.SetActive(true);
            ShakeText(loginResultText);
            return;
        }

        // �Էµ� ��й�ȣ�� �ؽ�
        string hashedInputPassword = HashPassword(loginInputField.PasswordInput.text);


        // ����� �ؽð��� ��
        if (loginInputField.UsernameInput.text == PlayerPrefs.GetString("Username")
            && hashedInputPassword == PlayerPrefs.GetString("Password"))
        {
            loginResultText.text = "�α��� ����!";
            loginResultText.gameObject.SetActive(true);
            SceneManager.LoadScene(1);
        }
        else
        {
            loginResultText.text = "���̵� �Ǵ� ��й�ȣ�� Ʋ���ϴ�.";
            loginResultText.gameObject.SetActive(true);
            ShakeText(loginResultText);
        }
    }

    //ȸ������ �õ�
    private void OnClickSignUp()
    {
        if (string.IsNullOrEmpty(registerInputField.UsernameInput.text))
        {
            registerResultText.text = "���̵� �Է��ϼ���.";
            registerResultText.gameObject.SetActive(true);
            ShakeText(registerResultText);
            return;
        }

        if (string.IsNullOrEmpty(registerInputField.PasswordInput.text))
        {
            registerResultText.text = "��й�ȣ�� �Է��ϼ���.";
            registerResultText.gameObject.SetActive(true);
            ShakeText(registerResultText);
            return;
        }

        if (registerInputField.PasswordInput.text != registerInputField.PasswordCheckInput.text)
        {
            registerResultText.text = "��й�ȣ�� ��ġ���� �ʽ��ϴ�.";
            registerResultText.gameObject.SetActive(true);
            ShakeText(registerResultText);
            return;
        }

        // ��й�ȣ ��ȣȭ
        string hashedPassword = HashPassword(registerInputField.PasswordInput.text);


        PlayerPrefs.SetString("Username", registerInputField.UsernameInput.text);
        PlayerPrefs.SetString("Password", hashedPassword);
        PlayerPrefs.Save();
        OnClickBackToLogin();

        loginInputField.UsernameInput.text = PlayerPrefs.GetString("Username");
        loginInputField.PasswordInput.text = PlayerPrefs.GetString("Password");
    }

    private string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2")); // 16���� ���ڿ��� ��ȯ
            }
            return builder.ToString();
        }
    }


    private void ShakeText(TextMeshProUGUI text)
    {
        RectTransform rectTransform = text.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            rectTransform.DOShakeAnchorPos(0.5f, new Vector2(10f, 0f), 10, 90, false, true);
        }
    }


    private void LoginCheck()
    {
        string id = loginInputField.UsernameInput.text;
        string password = loginInputField.PasswordInput.text;

        loginButton.interactable = !string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(password);
    }
}
