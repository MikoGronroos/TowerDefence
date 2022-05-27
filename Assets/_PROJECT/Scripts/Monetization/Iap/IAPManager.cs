using Finark.Events;
using System.Collections.Generic;
using UnityEngine.Purchasing;
using UnityEngine;

public class IAPManager : MonoBehaviour
{
    [SerializeField] private PlayFabCurrencyEventChannel playFabCurrencyEventChannel;

    private const string hardCurrency100 = "com.GronroosGames.TowerDefense.handfulofgems";
    private const string hardCurrency500 = "com.GronroosGames.TowerDefense.pouchofgems";
    private const string hardCurrency1000 = "com.GronroosGames.TowerDefense.truckloadofgems";

    private const string removeads = "com.GronroosGames.TowerDefense.removeads";

    public void OnPurchaseComplete(Product product)
    {

        if(product.definition.id == hardCurrency100) playFabCurrencyEventChannel.ChangeAmountOfHardCurrency?.Invoke(new Dictionary<string, object> { { "Amount", 100 } });

        if (product.definition.id == hardCurrency500) playFabCurrencyEventChannel.ChangeAmountOfHardCurrency?.Invoke(new Dictionary<string, object> { { "Amount", 500 } });

        if (product.definition.id == hardCurrency1000) playFabCurrencyEventChannel.ChangeAmountOfHardCurrency?.Invoke(new Dictionary<string, object> { { "Amount", 1000 } });

        if (!AccountManager.Instance.CurrentAccount.AdsRemoved) AccountManager.Instance.CurrentAccount.AdsRemoved = true;
        AccountManager.Instance.SaveData();

        Debug.Log(product.definition.id + " purchase was succesful!");
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(product.definition.id + " failed because " + failureReason);
    }

}
