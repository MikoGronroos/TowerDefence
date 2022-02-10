using UnityEngine;
using TMPro;

public class PlayFabCurrencyUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI hardCurrencyText;
    [SerializeField] private TextMeshProUGUI softCurrencyText;

    public void SetSoftCurrencyText(string text)
    {
        softCurrencyText.text = text;
    }

    public void SetHardCurrencyText(string text)
    {
        hardCurrencyText.text = text;
    }

}
