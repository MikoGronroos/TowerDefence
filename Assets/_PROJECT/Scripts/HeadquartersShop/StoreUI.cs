using Finark.Events;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PlayFab.ClientModels;

public class StoreUI : MonoBehaviour
{

	[SerializeField] private GameObject storeSlotPrefab;
    [SerializeField] private GameObject bundleSlotPrefab;

    [SerializeField] private Transform storeSlotParent;
    [SerializeField] private Transform bundleSlotParent;

    [SerializeField] private Button storeButton;

    [SerializeField] private StoreEventChannel storeEventChannel;
    [SerializeField] private PlayFabCurrencyEventChannel playFabCurrencyEventChannel;

    [Header("Bundle UI")]
    [SerializeField] private GameObject bundlePopup;
    [SerializeField] private TextMeshProUGUI bundleNameText;

    private List<GameObject> _storeSlots = new List<GameObject>();

    private void Awake()
    {
        storeButton.onClick.AddListener(()=> {
            storeEventChannel.OnStoreOpened?.Invoke();
        });
    }

    private void OnEnable()
    {
        storeEventChannel.StoreItemsFetched += StoreItemsFetched;
        storeEventChannel.BundleFetched += BundleFetched;
    }

    private void OnDisable()
    {
        storeEventChannel.StoreItemsFetched -= StoreItemsFetched;
        storeEventChannel.BundleFetched -= BundleFetched;
    }

    private void StoreItemsFetched(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {
        List<StoreItem> items = (List<StoreItem>)args["StoreItems"];

        ClearUI();

        DrawUI(items, callback);

    }

    private void BundleFetched(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {

        CatalogItem catalogItem = (CatalogItem)args["Bundle"];
        var bundle = catalogItem.Bundle;

        bundleNameText.text = catalogItem.DisplayName;

    }

    private void ClearUI()
    {
        foreach (var item in _storeSlots)
        {
            Destroy(item);
        }

        _storeSlots.Clear();
    }

    private void DrawUI(List<StoreItem> items, Action<Dictionary<string, object>> callback)
    {
        foreach (var item in items)
        {
            GameObject slot = Instantiate(storeSlotPrefab, storeSlotParent);

            _storeSlots.Add(slot);

            var storeSlot = slot.GetComponent<StoreSlot>();

            storeSlot.Initialize(item, callback);

        }
    }

}
