using Finark.Events;
using Finark.Utils;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop Item/Barrack")]
public class ShopItemBarrack : ShopItem
{

    [SerializeField] private ShopEventChannel shopEventChannel;

    public bool PlaceOnLocalBoard;
    
    public override void Buy()
    {
        BuyAction();
    }

    public override void BuyAction()
    {
        GameObject go = Instantiate(DragManager.Instance.GetDragObject());
        if (ItemPrefab.TryGetComponent(out Turret turret))
        {
            RangeVisualisation.Instance.DrawCircle(go, turret.GetTurretExecutable().Range.BaseValue, 0.25f);
        }
        go.transform.position = MyUtils.GetMouseWorldPosition();
        go.transform.rotation = Quaternion.identity;

        if (go.TryGetComponent(out ShopDragObject drag))
        {
            shopEventChannel?.OnEnteredDragging();
            drag.Setup(GraphicsManager.Instance.GetSprite(SkinManager.Instance.GetGraphicKeyWithMainKey(IconMainKey)), this);
        }

    }


}
