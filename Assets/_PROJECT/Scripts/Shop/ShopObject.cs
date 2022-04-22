using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Finark.Utils;

public class ShopObject : MonoBehaviour, IBeginDragHandler, IDragHandler
{

    [SerializeField] private TextMeshProUGUI costText;

    [SerializeField] private Image itemIcon;

    [SerializeField] private GameObject dragObject;

    private ShopItem _thisItem;

    //Called from shop button
    public void BuyThisItem() 
    {
        _thisItem.Buy();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GameObject go = Instantiate(dragObject);
        go.transform.position = MyUtils.GetMouseWorldPosition();
        go.transform.rotation = Quaternion.identity;

        if (go.TryGetComponent(out ShopDragObject drag))
        {
            drag.Setup(GraphicsManager.Instance.GetSprite(SkinManager.Instance.GetGraphicKeyWithMainKey(_thisItem.IconMainKey)), _thisItem);
        }

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
