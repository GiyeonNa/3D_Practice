using UnityEngine;
using UnityEngine.UI;

public class UI_Credit : UI_Popup
{
    [SerializeField]
    private Button Button;

    private void Awake()
    {
        Button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        Close();
    }
}
