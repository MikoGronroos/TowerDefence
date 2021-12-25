using System.Collections.Generic;
using UnityEngine;

public class VirtualCurrencyManager : MonoBehaviourSingleton<VirtualCurrencyManager>
{

    private VirtualCurrencyManagerUI _virtualCurrencyManagerUI;

    [SerializeField] private int currentCurrency;
    [SerializeField] private int maxCurrency;

    [SerializeField] private int currentIncome;
    [SerializeField] private float incomeInterval;
    private float _timeLeft;
    private bool _timerActive;

    private void Awake()
    {
        _virtualCurrencyManagerUI = GetComponent<VirtualCurrencyManagerUI>();
    }

    private void Update()
    {

        if (!_timerActive) return;

        if (_timeLeft > 0)
        {
            _timeLeft -= Time.deltaTime;
            _virtualCurrencyManagerUI.UpdatePlayerIncomeProgressBar(_timeLeft, incomeInterval);
        }
        else
        {
            OnTimerTimeEnd();
        }
    }

    private void OnTimerTimeEnd()
    {
        GiveIncome();
        _timeLeft = incomeInterval;
    }

    public void SetInterval(float value)
    {
        incomeInterval = value;
        _timerActive = true;
    }

    #region Currency

    public void RemoveCurrency(int value)
    {
        EventManager.InvokeEvent("OnMoneyUsed", new Dictionary<string, object> { { "money", value } });
        var currency = Mathf.Clamp(currentCurrency - value, 0, maxCurrency);
        SetCurrency(currency);
    }

    public void AddCurrency(int value)
    {
        var currency = Mathf.Clamp(currentCurrency + value, 0, maxCurrency);
        SetCurrency(currency);
    }

    public void SetCurrency(int value)
    {
        currentCurrency = value;
        _virtualCurrencyManagerUI.UpdatePlayerCurrency(currentCurrency);
    }

    public bool CheckIfPlayerHasEnoughCurrency(int value)
    {
        if (currentCurrency >= value)
        {
            return true;
        }
        return false;
    }

    #endregion

    #region Income

    public void RemoveIncome(int value)
    {
        var income = currentIncome - value;
        SetIncome(income);
    }

    public void AddIncome(int value)
    {
        var income = currentIncome + value;
        SetIncome(income);
    }

    public void SetIncome(int value)
    {
        currentIncome = value;
        _virtualCurrencyManagerUI.UpdatePlayerIncome(currentIncome);
    }

    public void GiveIncome()
    {
        AddCurrency(currentIncome);
    }

    #endregion

}
