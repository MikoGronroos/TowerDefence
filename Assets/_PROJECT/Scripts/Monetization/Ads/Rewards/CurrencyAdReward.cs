using Finark.Events;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Monetization/Advertisement/Currency Rewarded")]
public class CurrencyAdReward : AdReward
{

    public int SoftCurrencyReward;
    public int HardCurrencyReward;

    [SerializeField] private PlayFabCurrencyEventChannel playFabCurrencyEventChannel;

    public override void OnAdWatched()
    {
        if(SoftCurrencyReward > 0) playFabCurrencyEventChannel.ChangeAmountOfSoftCurrency?.Invoke(new Dictionary<string, object> { { "Amount", SoftCurrencyReward } });
        if(HardCurrencyReward > 0) playFabCurrencyEventChannel.ChangeAmountOfHardCurrency?.Invoke(new Dictionary<string, object> { { "Amount", HardCurrencyReward } });
    }

}