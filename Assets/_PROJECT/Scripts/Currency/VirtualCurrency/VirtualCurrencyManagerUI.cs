using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class VirtualCurrencyManagerUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI currencyText;

    [SerializeField] private TextMeshProUGUI incomeText;

    [SerializeField] private Image incomeIntervalProgressBar;

    [SerializeField] private string suffix;

    public void UpdatePlayerCurrency(Dictionary<string, object> args)
    {

        Debug.Log("Called");

        int value = (int)args["Currency"];

        currencyText.text = $"{value}{suffix}";
    }

    public void UpdatePlayerIncome(Dictionary<string, object> args)
    {

        int value = (int)args["Income"];

        incomeText.text = $"{value}{suffix}";
    }

    public void UpdatePlayerIncomeProgressBar(Dictionary<string, object> args)
    {

        float current = (float)args["TimeLeft"];
        float max = (float)args["IncomeInterval"];

        incomeIntervalProgressBar.fillAmount = current / max;
    }

}
