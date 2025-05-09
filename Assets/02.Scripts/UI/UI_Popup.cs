using System;
using UnityEngine;

public class UI_Popup : MonoBehaviour
{
    private Action closeCallback;

    public virtual void Close()
    {
        closeCallback?.Invoke();
        gameObject.SetActive(false);
    }

    public void Open(Action closeCallback = null)
    {
        this.closeCallback = closeCallback;
        gameObject.SetActive(true);
    }
}
