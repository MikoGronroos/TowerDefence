using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Finark.Events;

public class PlayFabStore : MonoBehaviour
{

    [SerializeField] private List<StoreItem> storeItems = new List<StoreItem>();

    [SerializeField] private string catalogVersion;

    [SerializeField] private StoreEventChannel storeEventChannel;
    [SerializeField] private PlayFabCurrencyEventChannel playFabCurrencyEventChannel;

    private void Start()
    {
        GetItems();
    }

    #region GetItems

    public void GetItems()
    {
        GetCatalogItemsRequest request = new GetCatalogItemsRequest();

        request.CatalogVersion = catalogVersion;

        PlayFabClientAPI.GetCatalogItems(request, Result, error => 
        {
            Debug.LogError(error.ErrorMessage);
        });
    }

    private void Result(GetCatalogItemsResult result)
    {
        List<CatalogItem> Items = result.Catalog;

        foreach (CatalogItem item in Items)
        {

            var bundle = item.Bundle;

            if (bundle != null)
            {

            }

            StoreItem storeItem = new StoreItem();

            storeItem.DisplayName = item.DisplayName;

            storeItem.Description = item.Description;

            if (item.Tags.Contains("sc"))
            {
                storeItem.Price = item.VirtualCurrencyPrices["SC"];
                storeItem.currencyType = CurrencyType.SoftCurrency;
            }
            else if (item.Tags.Contains("hc"))
            {
                storeItem.Price = item.VirtualCurrencyPrices["HC"];
                storeItem.currencyType = CurrencyType.HardCurrency;
            }

            storeItem.Icon = Resources.Load<Sprite>(item.ItemImageUrl);

            storeItem.ID = item.ItemId;

            storeItems.Add(storeItem);

            storeEventChannel.ItemFetched?.Invoke(new Dictionary<string, object> { {"Item", storeItem} }, BuyItem);

        }

    }

    #endregion

    #region BuyItem

    private void BuyItem(Dictionary<string, object> args)
    {

        StoreItem item = (StoreItem)args["Item"];

        PurchaseItemRequest request = new PurchaseItemRequest();

        request.CatalogVersion = catalogVersion;

        string currencyType = "";

        switch (item.currencyType)
        {
            case CurrencyType.HardCurrency:
                currencyType = "HC";
                break;
            case CurrencyType.SoftCurrency:
                currencyType = "SC";
                break;
            case CurrencyType.RealMoney:
                break;
        }

        request.VirtualCurrency = currencyType;


        request.ItemId = item.ID;
        request.Price = (int)item.Price;

        PlayFabClientAPI.PurchaseItem(request,
            result => 
            {

                playFabCurrencyEventChannel.RefreshHardAndSoftCurrencies?.Invoke();

            },
            error => 
            {
                Debug.LogError(error.ErrorMessage);
            }
        );
    }
    #endregion
}

[System.Serializable]
public class StoreItem
{

    public string DisplayName;

    public string Description;

    public uint Price;

    public Sprite Icon;

    public string ID;

    public CurrencyType currencyType;

}

public enum CurrencyType
{
    HardCurrency,
    SoftCurrency,
    RealMoney
}