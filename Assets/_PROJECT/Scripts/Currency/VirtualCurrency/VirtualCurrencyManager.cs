using Finark.Events;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCurrencyManager : MonoBehaviourSingleton<VirtualCurrencyManager>
{

    [SerializeField] private float currentCurrency;
    [SerializeField] private int maxCurrency;

    [SerializeField] private float currentIncome;
    [SerializeField] private float incomeInterval;

    [SerializeField] private CurrencyEventChannel currencyEventChannel;

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
            currencyEventChannel?.OnCurrencyIntervalUpdate(new Dictionary<string, object> { { "TimeLeft", _timeLeft}, { "IncomeInterval", incomeInterval } });
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

        currencyEventChannel.OnMoneyChanged?.Invoke(new Dictionary<string, object> { { "money", value } });

        var currency = Mathf.Clamp(currentCurrency - value, 0, maxCurrency);
        SetCurrency(currency);

    }

    public void AddCurrency(float value)
    {
        var currency = Mathf.Clamp(currentCurrency + value, 0, maxCurrency);
        SetCurrency(currency);
    }

    public void SetCurrency(float value)
    {
        currentCurrency = value;
        currencyEventChannel?.OnCurrencyChanged(new Dictionary<string, object> { { "Currency", currentCurrency } });
    }

    public bool CheckIfPlayerHasEnoughCurrency(float value)
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

    public void AddIncome(float value)
    {
        var income = currentIncome + value;
        SetIncome(income);
    }

    public void SetIncome(float value)
    {
        currentIncome = value;
        UpdateIncome();
    }

    public void GiveIncome()
    {
        AddCurrency(currentIncome);
    }

    private void UpdateIncome()
    {
        currencyEventChannel?.OnCurrencyIncomeChanged(new Dictionary<string, object> { { "Income", currentIncome } });
    }

    #endregion
}
