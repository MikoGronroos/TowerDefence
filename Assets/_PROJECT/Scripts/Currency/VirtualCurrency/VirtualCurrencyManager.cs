using Finark.Events;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCurrencyManager : MonoBehaviourSingleton<VirtualCurrencyManager>
{

    [SerializeField] private int currentCurrency;
    [SerializeField] private int maxCurrency;

    [SerializeField] private CustomFloat currentIncome;
    [SerializeField] private float incomeInterval;

    [SerializeField] private PlayerEventChannel playerEventChannel;

    private float _timeLeft;
    private bool _timerActive;

    private void Start()
    {
        _timeLeft = incomeInterval;
    }

    private void Update()
    {

        if (!_timerActive) return;

        if (_timeLeft > 0)
        {
            _timeLeft -= Time.deltaTime;
            playerEventChannel?.OnPlayerCurrencyIntervalUpdate(new Dictionary<string, object> { { "TimeLeft", _timeLeft}, { "IncomeInterval", incomeInterval } });
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
        playerEventChannel?.OnPlayerCurrencyChanged(new Dictionary<string, object> { { "Currency", currentCurrency } });
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
        var income = currentIncome.BaseValue - value;
        SetIncome((int)income);
    }

    public void AddIncome(int value)
    {
        var income = currentIncome.BaseValue + value;
        SetIncome((int)income);
    }

    public void SetIncome(int value)
    {
        currentIncome.BaseValue = value;
        UpdateIncome();
    }

    public void GiveIncome()
    {
        AddCurrency((int)currentIncome.Value);
    }

    private void UpdateIncome()
    {
        currentIncome.Value = currentIncome.BaseValue;
        playerEventChannel?.OnPlayerCurrencyIncomeChanged(new Dictionary<string, object> { { "Income", (int)currentIncome.Value } });
    }

    #endregion
}
