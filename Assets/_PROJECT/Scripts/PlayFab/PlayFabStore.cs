using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Finark.Events;
using System.Linq;
using System;

public class PlayFabStore : MonoBehaviour
{

    [SerializeField] private List<StoreItem> storeItems = new List<StoreItem>();

    [SerializeField] private string catalogVersion;

    [SerializeField] private StoreEventChannel storeEventChannel;
    [SerializeField] private PlayFabCurrencyEventChannel playFabCurrencyEventChannel;

    private void OnEnable()
    {
        storeEventChannel.OnStoreOpened += GetItems;
    }

    private void OnDisable()
    {
        storeEventChannel.OnStoreOpened -= GetItems;
    }

    #region GetItems

    public void GetItems(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
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

        storeItems.Clear();

        List<CatalogItem> Items = result.Catalog;

        foreach (CatalogItem item in Items)
        {

            var bundle = item.Bundle;

            if (bundle != null)
            {
                storeEventChannel.BundleFetched?.Invoke(new Dictionary<string, object> { { "Bundle", item } });
                continue;
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

            SkinCustomData data = JsonUtility.FromJson<SkinCustomData>(item.CustomData);

            if (data != null)
            {
                storeItem.Icon = GraphicsManager.Instance.GetSprite(data.SkinName);
            }

            storeItem.SkinID = data.SkinName;

            storeItem.MainKey = data.MainKey;

            storeItem.ID = item.ItemId;

            storeItems.Add(storeItem);

        }

        storeEventChannel.StoreItemsFetched?.Invoke(new Dictionary<string, object> { {"StoreItems", storeItems} }, BuyItem);

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

                Debug.Log("Bought Item");

                PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
                {
                    FunctionName = "SetInstanceData",
                    FunctionParameter = new { ItemId = result.Items.First().ItemInstanceId, SkinId = item.SkinID, MainKey = item.MainKey }
                }, 
                result => { Debug.Log("Cloud script call succesful"); },
                failure =>{ });

                playFabCurrencyEventChannel.RefreshHardAndSoftCurrencies?.Invoke(null, (Dictionary<string, object> args) => 
                {
                    GetItems(null, null); 
                });

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

    public string SkinID;

    public string MainKey;

}

public enum CurrencyType
{
    HardCurrency,
    SoftCurrency,
    RealMoney
}