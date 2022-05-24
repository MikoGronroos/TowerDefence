using System.Collections.Generic;
using UnityEngine;
using Finark.Events;

public class IapRewards : MonoBehaviour
{

    [SerializeField] private PlayFabCurrencyEventChannel playFabCurrencyEventChannel;

	public void GiveHardCurrency(int amount)
    {
        playFabCurrencyEventChannel.ChangeAmountOfHardCurrency?.Invoke(new Dictionary<string, object> { { "Amount", amount } });
        AllPurchases();
    }

    public void RemoveAds()
    {
        AllPurchases();
    }

    private void AllPurchases()
    {
        if (!AccountManager.Instance.CurrentAccount.AdsRemoved)
        {
            AccountManager.Instance.CurrentAccount.AdsRemoved = true;
            AccountManager.Instance.SaveData();
        }
    }

}
