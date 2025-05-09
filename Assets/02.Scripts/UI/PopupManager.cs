using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public enum EPopupType
{
    None,
    UI_Option,
    UI_Credit,
    Inventory,
    GameOver
}

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance { get; private set; }
    

    //여기를 어드레서블로 변경?
    public List<UI_Popup> popups = new List<UI_Popup>();


    private Stack<UI_Popup> openedPopups = new Stack<UI_Popup>();


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Open(EPopupType type)
    {
        Open(type.ToString());
    }

    public void Open(EPopupType type, System.Action closeCallback = null)
    {
        foreach (var popup in popups)
        {
            if (popup.name == type.ToString())
            {
                popup.Open(closeCallback);
                openedPopups.Push(popup);
                return;
            }
        }
    }

    public void Open(string name)
    {
        foreach (var popup in popups)
        {
            if (popup.name == name)
            {
                popup.Open();
                openedPopups.Push(popup);
                return;
            }
        }
    }

    public void Close(EPopupType type)
    {
        foreach (var popup in popups)
        {
            if (popup.name == type.ToString())
            {
                popup.Close();
                openedPopups.Pop();
                return;
            }
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(openedPopups.Count > 0)
            {
                while(true)
                {
                    var popup = openedPopups.Pop();
                    bool open = popup.isActiveAndEnabled;
                    popup.Close();

                    if(open || openedPopups.Peek() == null)
                    {
                        break;
                    }

                    //var topPopup = openedPopups.Peek();
                    //bool isOpend = topPopup.isActiveAndEnabled;
                    //topPopup.Close();
                    //openedPopups.Pop();

                    //if (isOpend || openedPopups.Count == 0)
                    //{
                    //    break;
                    //}
                }
            }
            else
            {
                GameManager.Instance.Pause();
            }

        }
    }





}
