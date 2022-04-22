using Finark.Utils;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop Item/Building")]
public class ShopItemBuilding : ShopItem
{

    public bool PlaceOnLocalBoard;
    
    public override void Buy()
    {
        BuyAction();
    }

    public override void BuyAction()
    {
        GameObject go = Instantiate(DragManager.Instance.GetDragObject());
        go.transform.position = MyUtils.GetMouseWorldPosition();
        go.transform.rotation = Quaternion.identity;

        if (go.TryGetComponent(out ShopDragObject drag))
        {
            drag.Setup(GraphicsManager.Instance.GetSprite(SkinManager.Instance.GetGraphicKeyWithMainKey(IconMainKey)), this);
        }

    }


}
