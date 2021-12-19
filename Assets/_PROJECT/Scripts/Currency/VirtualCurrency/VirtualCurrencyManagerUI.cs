using UnityEngine;
using TMPro;

public class VirtualCurrencyManagerUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI currencyText;
    [SerializeField] private string suffix;

    public void UpdatePlayerCurrency(int value)
    {
        currencyText.text = $"{value}{suffix}";
    }

}
