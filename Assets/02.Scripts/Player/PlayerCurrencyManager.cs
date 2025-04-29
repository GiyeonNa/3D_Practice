using System;
using UnityEngine;

public class PlayerCurrencyManager : MonoBehaviour
{
    public static PlayerCurrencyManager Instance { get; private set; }
    public event Action<int> OnCurrencyChanged;
    
    private int currency;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }


    public int GetCurrency()
    {
        return currency;
    }


    public void AddCurrency(int amount)
    {
        if (amount < 0)
            return;

        currency += amount;
        OnCurrencyChanged?.Invoke(currency); 
    }

    public bool SubtractCurrency(int amount)
    {
        if (amount < 0)
            return false;

        if (currency >= amount)
        {
            currency -= amount;
            OnCurrencyChanged?.Invoke(currency); // 이벤트 호출
            return true;
        }
        else
            return false;
    }
}
