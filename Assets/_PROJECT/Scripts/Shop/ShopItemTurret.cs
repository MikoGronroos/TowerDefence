using UnityEngine;

[CreateAssetMenu(menuName = "Shop Item/Turret")]
public class ShopItemTurret : ShopItemBuilding
{

    public override void Buy() 
    {
        BuyAction();
    }

    public override void BuyAction()
    {
        base.BuyAction();
    }
}
