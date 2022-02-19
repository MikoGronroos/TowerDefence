using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;
using Finark.Events;

public class VirtualCurrencyManagerUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI currencyText;

    [SerializeField] private TextMeshProUGUI incomeText;

    [SerializeField] private Image incomeIntervalProgressBar;

    [SerializeField] private string suffix;

    [SerializeField] private CurrencyEventChannel currencyEventChannel;

    private void OnEnable()
    {
        currencyEventChannel.OnCurrencyIncomeChanged += UpdatePlayerIncome;
        currencyEventChannel.OnCurrencyChanged += UpdatePlayerCurrency;
        currencyEventChannel.OnCurrencyIntervalUpdate += UpdatePlayerIncomeProgressBar;
    }

    private void OnDisable()
    {
        currencyEventChannel.OnCurrencyIncomeChanged -= UpdatePlayerIncome;
        currencyEventChannel.OnCurrencyChanged -= UpdatePlayerCurrency;
        currencyEventChannel.OnCurrencyIntervalUpdate -= UpdatePlayerIncomeProgressBar;
    }

    private void UpdatePlayerCurrency(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {

        int value = (int)args["Currency"];

        currencyText.text = $"{value}{suffix}";

    }

    private void UpdatePlayerIncome(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {

        int value = (int)args["Income"];

        incomeText.text = $"{value}{suffix}";
    }

    private void UpdatePlayerIncomeProgressBar(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {

        float current = (float)args["TimeLeft"];
        float max = (float)args["IncomeInterval"];

        incomeIntervalProgressBar.fillAmount = current / max;
    }

}
