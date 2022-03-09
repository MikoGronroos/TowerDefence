using Finark.Events;
using System;
using System.Collections.Generic;
using UnityEngine;

public class StoreUI : MonoBehaviour
{

	[SerializeField] private GameObject storeSlotPrefab;

    [SerializeField] private Transform parent;

	[SerializeField] private StoreEventChannel storeEventChannel;

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

        storeSlot.Initialize(item, callback);

    }
}
