using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public enum GameState
{
    Ready,
    Run,
    Over
}
public class GameManager : MonoBehaviour
{
    public GameState State;
    public static GameManager Instance;
    public float WaitTime;

    private void Awake()
    {
        Instance = this;

        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Start()
    {
        State = GameState.Ready;
        Time.timeScale = 0f;

        StartCoroutine(Play());
    }

    public void Pause()
    {
        State = GameState.Ready;
        Time.timeScale = 0;
        //PopupManager.Instance.Open("UI_Option");
        PopupManager.Instance.Open(EPopupType.UI_Option, Continue);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Continue()
    {
        State = GameState.Run;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }


    private IEnumerator Play()
    {
        float remainingTime = WaitTime;
        while (remainingTime > 0)
        {
            PlayerUI.Instance.SetCountdownText(Mathf.CeilToInt(remainingTime));
            yield return new WaitForSecondsRealtime(1f);
            remainingTime -= 1f;
        }

        PlayerUI.Instance.SetCountdownText(0); // Clear countdown text
        State = GameState.Run;
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        PlayerUI.Instance.SetGameOver();
        State = GameState.Over;
        Time.timeScale = 0f;
    }

}