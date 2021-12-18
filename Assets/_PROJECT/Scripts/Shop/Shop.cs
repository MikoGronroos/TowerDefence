using UnityEngine;

public class Shop : MonoBehaviour
{

    [SerializeField] private ShopInventory unitShopInventory;

    [SerializeField] private ShopInventory turretShopInventory;

    private ShopUI _shopUI;

    private void Awake()
    {
        _shopUI = GetComponent<ShopUI>();
    }

    private void Start()
    {
        InitializeShop();
    }

    private void InitializeShop()
    {
        foreach (var item in unitShopInventory.Inventory)
        {
            _shopUI.DrawShopItem(item);
        }
        foreach (var item in turretShopInventory.Inventory)
        {
            _shopUI.DrawShopItem(item);
        }
    }

}
