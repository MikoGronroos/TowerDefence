using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VirtualCurrencyManagerUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI currencyText;

    [SerializeField] private TextMeshProUGUI incomeText;

    [SerializeField] private Image incomeIntervalProgressBar;

    [SerializeField] private string suffix;

    public void UpdatePlayerCurrency(int value)
    {
        currencyText.text = $"{value}{suffix}";
    }

    public void UpdatePlayerIncome(int value)
    {
        incomeText.text = $"{value}{suffix}";
    }

    public void UpdatePlayerIncomeProgressBar(float current, float max)
    {
        incomeIntervalProgressBar.fillAmount = current / max;
    }

}
