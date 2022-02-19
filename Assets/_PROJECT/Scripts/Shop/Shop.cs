using UnityEngine;
using System.Collections.Generic;
using Finark.Events;
using System;

public class Shop : MonoBehaviourSingleton<Shop>
{

    [SerializeField] private ShopInventory unitShopInventory;

    [SerializeField] private ShopInventory turretShopInventory;

    [SerializeField] private ShopEventChannel shopEventChannel;
    [SerializeField] private PlayerEventChannel playerEventChannel;

    private void OnEnable()
    {
        playerEventChannel.OnPlayerLevelUp += RefreshShop;
    }

    private void OnDisable()
    {
        playerEventChannel.OnPlayerLevelUp -= RefreshShop;
    }

    private void Start()
    {
        InitializeShop();
    }

    private void InitializeShop()
    {
        RefreshShop(null, null);
    }

    private void RefreshShop(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {

        shopEventChannel?.RefreshShop(new Dictionary<string, object> { {"Units", unitShopInventory }, { "Turrets", turretShopInventory } });

    }

}
