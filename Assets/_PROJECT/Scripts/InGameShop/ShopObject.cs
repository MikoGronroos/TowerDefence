using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI incomeAddonText;

    [SerializeField] private Image itemIcon;

    private ShopItem _thisItem;

    //Called from shop button
    public void BuyThisItem() 
    {
        _thisItem.Buy();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        InGameShopTooltipManager.Instance.DisableTooltip();
        _thisItem.Buy();
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    #region Tooltip

    public void OnPointerDown(PointerEventData eventData)
    {

        if (_thisItem.ItemPrefab.TryGetComponent(out Turret turret))
        {
            InGameShopTooltipManager.Instance.StartEnablingTooltip(transform.position, turret, null);
        }

        if (_thisItem.ItemPrefab.TryGetComponent(out Unit unit))
        {
            InGameShopTooltipManager.Instance.StartEnablingTooltip(transform.position, null, unit);
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InGameShopTooltipManager.Instance.DisableTooltip();
    }

    #endregion

    public void SetCostText(string content)
    {
        costText.text = content;
    }

    public void SetIncomeAddonText(string content, Color color)
    {
        incomeAddonText.color = color;
        incomeAddonText.text = content;
    }

    public void SetItemIcon(Sprite icon)
    {
        itemIcon.sprite = icon;
    }

    public void SetThisItem(ShopItem item)
    {
        _thisItem = item;
    }

}
