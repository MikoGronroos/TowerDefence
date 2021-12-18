using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{

    [SerializeField] private Transform unitShopItemParent;

    [SerializeField] private Transform turretShopItemParent;

    [SerializeField] private GameObject shopItemPrefab;

    [SerializeField] private GameObject shopGameObject;

    [SerializeField] private string suffix;

    [SerializeField] private Button closeShopButton;

    [SerializeField] private Button openShopButton;

    [SerializeField] private List<GameObject> drawnShopItems = new List<GameObject>();

    private void Awake()
    {
        closeShopButton.onClick.AddListener(() => {
            shopGameObject.SetActive(false);
        });
        openShopButton.onClick.AddListener(() => {
            shopGameObject.SetActive(true);
        });
    }

    public void DrawShopItem(ShopItem item)
    {

        Transform parent = null;

        if (item is ShopItemUnit)
        {
            parent = unitShopItemParent;
        }
        else
        {
            parent = turretShopItemParent;
        }

        GameObject shopItem = Instantiate(shopItemPrefab, parent);
        drawnShopItems.Add(shopItem);

        var itemScript = shopItem.GetComponent<ShopObject>();

        itemScript.SetCostText(item.Cost.ToString() + " " + suffix);
        itemScript.SetItemIcon(item.Icon);
        itemScript.SetThisItem(item);

    }

}
