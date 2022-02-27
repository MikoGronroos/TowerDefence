using UnityEngine;
using TMPro;
using Finark.Events;
using System.Collections.Generic;
using System;

public class PlayFabCurrencyUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI hardCurrencyText;
    [SerializeField] private TextMeshProUGUI softCurrencyText;

    [SerializeField] private PlayFabCurrencyEventChannel playFabCurrencyEventChannel;

    private void OnEnable()
    {
        playFabCurrencyEventChannel.AmountOfSoftCurrencyChanged += SetSoftCurrencyText;
        playFabCurrencyEventChannel.AmountOfHardCurrencyChanged += SetHardCurrencyText;
    }

    private void OnDisable()
    {
        playFabCurrencyEventChannel.AmountOfSoftCurrencyChanged -= SetSoftCurrencyText;
        playFabCurrencyEventChannel.AmountOfHardCurrencyChanged -= SetHardCurrencyText;
    }

    private void SetSoftCurrencyText(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {

        var amount = (int)args["TotalAmount"];

        softCurrencyText.text = amount.ToString();
    }

    private void SetHardCurrencyText(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {
        var amount = (int)args["TotalAmount"];

        hardCurrencyText.text = amount.ToString();
    }

}
