using UnityEngine;
using System.Collections.Generic;

public class Shop : MonoBehaviourSingleton<Shop>
{

    [SerializeField] private ShopInventory unitShopInventory;

    [SerializeField] private ShopInventory turretShopInventory;

    //[SerializeField] private EventChannel refreshShopChannel;

    private void OnEnable()
    {
        PlayerLevel.OnLevelUpEvent += RefreshShop;
    }

    private void OnDisable()
    {
        PlayerLevel.OnLevelUpEvent -= RefreshShop;
    }

    private void Start()
    {
        InitializeShop();
    }

    private void InitializeShop()
    {
        RefreshShop();
    }

    private void RefreshShop()
    {

        //refreshShopChannel.RaiseEvent(new Dictionary<string, object> { {"Units", unitShopInventory }, { "Turrets", turretShopInventory } });

    }

}
