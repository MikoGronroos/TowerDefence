using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class PlayFabStore : MonoBehaviour
{

    [SerializeField] private List<StoreItem> storeItems = new List<StoreItem>();

    private void Start()
    {
        GetItems();
    }

    public void GetItems()
    {
        GetCatalogItemsRequest request = new GetCatalogItemsRequest();

        request.CatalogVersion = "1.0";

        PlayFabClientAPI.GetCatalogItems(request, Result, Error);
    }

    private void Error(PlayFabError error)
    {
    }

    private void Result(GetCatalogItemsResult result)
    {
        List<CatalogItem> Items = result.Catalog;

        foreach (CatalogItem item in Items)
        {
            StoreItem storeItem = new StoreItem();

            storeItem.Price = item.VirtualCurrencyPrices["SC"];

            storeItems.Add(storeItem);

        }

    }
}

[System.Serializable]
public class StoreItem
{
    public uint Price;
}