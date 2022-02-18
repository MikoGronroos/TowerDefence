using UnityEngine;

public class Shop : MonoBehaviourSingleton<Shop>
{

    [SerializeField] private ShopInventory unitShopInventory;

    [SerializeField] private ShopInventory turretShopInventory;

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

        //_shopUI.EraseDrawnShopItems();

        foreach (var item in unitShopInventory.Inventory)
        {
            if (PlayerLevel.Instance.GetCurrentLevel() >= item.LevelToUnlock)
            {
                //_shopUI.DrawShopItem(item);
            }
        }
        foreach (var item in turretShopInventory.Inventory)
        {
            if (PlayerLevel.Instance.GetCurrentLevel() >= item.LevelToUnlock)
            {
                //_shopUI.DrawShopItem(item);
            }
        }
    }

}
