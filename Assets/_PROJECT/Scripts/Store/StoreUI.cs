using Finark.Events;
using System;
using System.Collections.Generic;
using UnityEngine;

public class StoreUI : MonoBehaviour
{

	[SerializeField] private GameObject storeSlotPrefab;

    [SerializeField] private Transform parent;

	[SerializeField] private StoreEventChannel storeEventChannel;
    [SerializeField] private PlayFabCurrencyEventChannel playFabCurrencyEventChannel;

    [Header("Slot Text")]
    [SerializeField] private Color allowedToBuyColor;
    [SerializeField] private Color notAllowedToBuyColor;

    private void OnEnable()
    {
        storeEventChannel.ItemFetched += ItemBought;
    }

    private void OnDisable()
    {
        storeEventChannel.ItemFetched -= ItemBought;
    }

    private void ItemBought(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {

        GameObject slot = Instantiate(storeSlotPrefab, parent);

        var storeSlot = slot.GetComponent<StoreSlot>();

        StoreItem item = (StoreItem)args["Item"];

        Color textColor = notAllowedToBuyColor;
        /*
        switch (item.currencyType)
        {
            case CurrencyType.HardCurrency:
                playFabCurrencyEventChannel.CheckIfPlayerHasEnoughHardCurrency?.Invoke(new Dictionary<string, object> { { "Cost", (int)item.Price } });
                break;
            case CurrencyType.SoftCurrency:
                playFabCurrencyEventChannel.CheckIfPlayerHasEnoughSoftCurrency?.Invoke(new Dictionary<string, object> { { "Cost", (int)item.Price } });
                break;
            case CurrencyType.RealMoney:
                textColor = allowedToBuyColor;
                break;
        }
        */
        storeSlot.Initialize(item, callback, textColor);

    }
}
