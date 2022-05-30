using Finark.Events;
using Finark.Utils;
using System.Collections;
using UnityEngine;

public class ShopDragObject : MonoBehaviour
{

    [SerializeField] private SpriteRenderer iconImage;

    [SerializeField] private ShopEventChannel shopEventChannel;

    private ShopItemBarrack _shopItem;

    public void Setup(Sprite icon, ShopItem item)
    {
        iconImage.sprite = icon;
        _shopItem = item as ShopItemBarrack;
        StartCoroutine(SyntheticDrag());
    }

    private void OnMouseDown()
    {
        
    }

    private void OnMouseUp()
    {
        if (VirtualCurrencyManager.Instance.CheckIfPlayerHasEnoughCurrency(_shopItem.Cost))
        {
            if (BuildingManager.Instance.Build(MyUtils.GetMouseWorldPosition(), _shopItem))
            {
                VirtualCurrencyManager.Instance.RemoveCurrency(_shopItem.Cost);
            }
        }
        shopEventChannel?.OnExitedDragging();
        Destroy(gameObject);
    }

    private void OnMouseDrag()
    {
        transform.position = MyUtils.GetMouseWorldPosition();
    }


    IEnumerator SyntheticDrag()
    {
        OnMouseDown();
        yield return null;

        while (Input.GetMouseButton(0))
        {
            OnMouseDrag();
            yield return null;
        }
        OnMouseUp();
    }
}
