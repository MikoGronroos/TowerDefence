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

            StoreItem storeItem = new StoreItem();

            storeItem.DisplayName = item.DisplayName;

            storeItem.Description = item.Description;

            if (item.Tags.Contains("sc"))
            {
                storeItem.Price = item.VirtualCurrencyPrices["SC"];
                storeItem.hardCurrencyItem = false;
            }
            else if (item.Tags.Contains("hc"))
            {
                storeItem.Price = item.VirtualCurrencyPrices["HC"];
                storeItem.hardCurrencyItem = true;
            }

            storeItem.Icon = Resources.Load<Sprite>(item.ItemImageUrl);

            storeItem.ID = item.ItemId;

            storeItems.Add(storeItem);

            storeEventChannel.BoughtItem?.Invoke(new Dictionary<string, object> { {"Item", storeItem} }, BuyItem);

        }

    }

    #endregion

    #region BuyItem

    private void BuyItem(Dictionary<string, object> args)
    {

        StoreItem item = (StoreItem)args["Item"];

        PurchaseItemRequest request = new PurchaseItemRequest();

        request.CatalogVersion = catalogVersion;

        request.VirtualCurrency = item.hardCurrencyItem ? "HC" : "SC";

        request.ItemId = item.ID;
        request.Price = (int)item.Price;

        PlayFabClientAPI.PurchaseItem(request,
            result => 
            {
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

    public bool hardCurrencyItem;

}