using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;

public class VirtualCurrencyManagerUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI currencyText;

    [SerializeField] private TextMeshProUGUI incomeText;

    [SerializeField] private Image incomeIntervalProgressBar;

    [SerializeField] private string suffix;

    [SerializeField] private PlayerEventChannel playerEventChannel;

    private void OnEnable()
    {
        playerEventChannel.OnPlayerCurrencyIncomeChanged += UpdatePlayerIncome;
        playerEventChannel.OnPlayerCurrencyChanged += UpdatePlayerCurrency;
        playerEventChannel.OnPlayerCurrencyIntervalUpdate += UpdatePlayerIncomeProgressBar;
    }

    private void OnDisable()
    {
        playerEventChannel.OnPlayerCurrencyIncomeChanged -= UpdatePlayerIncome;
        playerEventChannel.OnPlayerCurrencyChanged -= UpdatePlayerCurrency;
        playerEventChannel.OnPlayerCurrencyIntervalUpdate -= UpdatePlayerIncomeProgressBar;
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
