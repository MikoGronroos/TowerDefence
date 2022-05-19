using Finark.Events;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{

    [SerializeField] private Transform unitShopItemParent;

    [SerializeField] private Transform turretShopItemParent;

    [SerializeField] private Transform buildingShopItemParent;

    [SerializeField] private GameObject shopItemPrefab;

    [SerializeField] private GameObject shopPanel;

    [SerializeField] private string suffix;

    [SerializeField] private Button closeShopButton;

    [SerializeField] private Button openShopButton;

    [SerializeField] private ShopEventChannel shopEventChannel;

    [Header("Colours")]
    [SerializeField] private Color positiveIncomeColor;
    [SerializeField] private Color negativeIncomeColor;

    private List<GameObject> _drawnShopItems = new List<GameObject>();

    private void Awake()
    {
        closeShopButton.onClick.AddListener(() => {
            shopPanel.SetActive(false);
        });
        openShopButton.onClick.AddListener(() => {
            shopPanel.SetActive(true);
        });
    }

    private void OnEnable()
    {
        shopEventChannel.RefreshShop += RefreshShopUI;
        shopEventChannel.OnEnteredDragging += DraggingEntered;
        shopEventChannel.OnExitedDragging += DraggingExited;
    }

    private void OnDisable()
    {
        shopEventChannel.RefreshShop -= RefreshShopUI;
        shopEventChannel.OnEnteredDragging -= DraggingEntered;
        shopEventChannel.OnExitedDragging -= DraggingExited;
    }

    private void DrawShopItem(ShopItem item)
    {

        Transform parent = null;


        if (item is ShopItemUnit)
        {
            parent = unitShopItemParent;
        }
        else if (item is ShopItemTurret)
        {
            parent = turretShopItemParent;
        }
        else if (item is ShopItemBarrack)
        {
            parent = buildingShopItemParent;
        }

        GameObject shopItem = Instantiate(shopItemPrefab, parent);
        _drawnShopItems.Add(shopItem);

        var itemScript = shopItem.GetComponent<ShopObject>();

        itemScript.SetCostText(item.Cost.ToString() + " " + suffix);

        if (item is ShopItemUnit)
        {
            var newItem = (ShopItemUnit)item;

            if (newItem.IncomeAddonFromSpawning > 0)
            {
                itemScript.SetIncomeAddonText("<sprite index=0>" + newItem.IncomeAddonFromSpawning.ToString() + " " + suffix, positiveIncomeColor);
            }
            else
            {
                itemScript.SetIncomeAddonText("<sprite index=1>" + newItem.IncomeAddonFromSpawning.ToString() + " " + suffix, negativeIncomeColor);
            }

        }

        itemScript.SetItemIcon(GraphicsManager.Instance.GetSprite(SkinManager.Instance.GetGraphicKeyWithMainKey(item.IconMainKey)));
        itemScript.SetThisItem(item);

    }

    private void EraseDrawnShopItems()
    {
        foreach (var item in _drawnShopItems)
        {
            Destroy(item);
        }
        _drawnShopItems.Clear();
    }

    public void RefreshShopUI(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {

        var units = (ShopInventory)args["Units"];
        var turrets = (ShopInventory)args["Turrets"];
        var buildings = (ShopInventory)args["Buildings"];

        EraseDrawnShopItems();

        foreach (var item in units.Inventory)
        {
            if (PlayerLevel.Instance.GetCurrentLevel() >= item.LevelToUnlock)
            {
                DrawShopItem(item);
            }
        }

        foreach (var item in turrets.Inventory)
        {
            if (PlayerLevel.Instance.GetCurrentLevel() >= item.LevelToUnlock)
            {
                DrawShopItem(item);
            }
        }

        foreach (var item in buildings.Inventory)
        {
            if (PlayerLevel.Instance.GetCurrentLevel() >= item.LevelToUnlock)
            {
                DrawShopItem(item);
            }
        }
    }

    private void DraggingExited(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {
        shopPanel.SetActive(true);
    }

    private void DraggingEntered(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {
        shopPanel.SetActive(false);
    }

}
