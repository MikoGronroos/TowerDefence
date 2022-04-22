using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopObject : MonoBehaviour, IBeginDragHandler, IDragHandler
{

    [SerializeField] private TextMeshProUGUI costText;

    [SerializeField] private Image itemIcon;


    private ShopItem _thisItem;

    //Called from shop button
    public void BuyThisItem() 
    {
        _thisItem.Buy();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _thisItem.Buy();
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void SetCostText(string content)
    {
        costText.text = content;
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
