using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Option : UI_Popup
{
    [SerializeField]
    private Button resumeButton;
    [SerializeField]
    private Button restartButton;
    [SerializeField]
    private Button exitButton;
    [SerializeField]
    private Button creditButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(OnResumeButtonClick);
        restartButton.onClick.AddListener(OnRestartButtonClick);
        exitButton.onClick.AddListener(OnExitButtonClick);
        creditButton.onClick.AddListener(OnCreditButtonClick);
    }

    private void OnResumeButtonClick()
    {
        Debug.Log("Resume Button Clicked");
        Time.timeScale = 1f;
        PopupManager.Instance.Close(EPopupType.UI_Option);

    }

    private void OnRestartButtonClick()
    {
        Debug.Log("Restart Button Clicked");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnExitButtonClick()
    {
        Debug.Log("Exit Button Clicked");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void OnCreditButtonClick()
    {
        PopupManager.Instance.Open(EPopupType.UI_Credit);
    }
}
