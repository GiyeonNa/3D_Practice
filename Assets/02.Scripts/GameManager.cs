using System.Collections;
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
    }

    private void Start()
    {
        State = GameState.Ready;
        Time.timeScale = 0f;

        StartCoroutine(Play());
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