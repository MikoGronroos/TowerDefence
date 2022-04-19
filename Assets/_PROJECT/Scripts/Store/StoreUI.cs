using Finark.Events;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PlayFab.ClientModels;

public class StoreUI : MonoBehaviour
{

	[SerializeField] private GameObject storeSlotPrefab;
    [SerializeField] private GameObject bundleSlotPrefab;

    [SerializeField] private Transform storeSlotParent;
    [SerializeField] private Transform bundleSlotParent;

    [SerializeField] private StoreEventChannel storeEventChannel;
    [SerializeField] private PlayFabCurrencyEventChannel playFabCurrencyEventChannel;

    [Header("Bundle UI")]
    [SerializeField] private GameObject bundlePopup;
    [SerializeField] private TextMeshProUGUI bundleNameText;

    [Header("Slot Text")]
    [SerializeField] private Color allowedToBuyColor;
    [SerializeField] private Color notAllowedToBuyColor;

    private void OnEnable()
    {
        storeEventChannel.ItemFetched += ItemFetched;
        storeEventChannel.BundleFetched += BundleFetched;
    }

    private void OnDisable()
    {
        storeEventChannel.ItemFetched -= ItemFetched;
        storeEventChannel.BundleFetched -= BundleFetched;
    }

    private void ItemFetched(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {

        GameObject slot = Instantiate(storeSlotPrefab, storeSlotParent);

        var storeSlot = slot.GetComponent<StoreSlot>();

        StoreItem item = (StoreItem)args["Item"];

        Color textColor = notAllowedToBuyColor;

        storeSlot.Initialize(args, callback, textColor);

    }

    private void BundleFetched(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {

        CatalogItem catalogItem = (CatalogItem)args["Bundle"];
        var bundle = catalogItem.Bundle;

        bundleNameText.text = catalogItem.DisplayName;

    }

}
