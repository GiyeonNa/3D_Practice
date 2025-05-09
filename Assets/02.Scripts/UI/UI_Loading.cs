using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class UI_Loading : MonoBehaviour
{
    public int nextSceneIndex;

    [SerializeField]
    private Slider loadingBar;
    [SerializeField]
    private TextMeshProUGUI loadingText;

    private void Start()
    {
        loadingBar.value = 0f;
        loadingText.text = "Loading... 0%";
        StartCoroutine(LoadSceneAsync(nextSceneIndex));
    }

    private IEnumerator LoadSceneAsync(int sceneIndex)
    {
        AsyncOperation asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneIndex);
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            loadingBar.value = progress;
            loadingText.text = $"Loading... {Mathf.RoundToInt(progress * 100)}%";

            if(asyncOperation.progress > 0.2f)
            {
                //퍼센트에 따른 텍스트로 변경가능
            }

            if (asyncOperation.progress >= 0.9f)
            {
                loadingText.text = "Press any key to continue...";
                if (Input.anyKeyDown)
                {
                    asyncOperation.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }
}
