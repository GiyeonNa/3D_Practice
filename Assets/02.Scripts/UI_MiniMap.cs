using UnityEngine;
using UnityEngine.UI;

public class UI_MiniMap : MonoBehaviour
{
    [SerializeField]
    private Camera miniMapCamera;
    [SerializeField]
    private Button plusButton;
    [SerializeField]
    private Button minusButton;

    private void Awake()
    {
        plusButton.onClick.AddListener(OnPlusButtonClicked);
        minusButton.onClick.AddListener(OnMinusButtonClicked);
    }

    private void OnPlusButtonClicked()
    {
        miniMapCamera.orthographicSize -= 1f;
        if (miniMapCamera.orthographicSize < 1f)
            miniMapCamera.orthographicSize = 1f;
    }

    private void OnMinusButtonClicked()
    {
        miniMapCamera.orthographicSize += 1f;
        if (miniMapCamera.orthographicSize > 25f)
            miniMapCamera.orthographicSize = 25f;
    }
}
