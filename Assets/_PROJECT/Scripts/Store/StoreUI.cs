using Finark.Events;
using System;
using System.Collections.Generic;
using UnityEngine;

public class StoreUI : MonoBehaviour
{

	[SerializeField] private GameObject storeSlotPrefab;

	[SerializeField] private StoreEventChannel storeEventChannel;

    private void OnEnable()
    {
        storeEventChannel.BoughtItem += ItemBought;
    }

    private void OnDisable()
    {
        storeEventChannel.BoughtItem -= ItemBought;
    }

    private void ItemBought(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {

        var slot = storeSlotPrefab.GetComponent<StoreSlot>();

        StoreItem item = (StoreItem)args["Item"];

        slot.Initialize(item, callback);

    }
}
