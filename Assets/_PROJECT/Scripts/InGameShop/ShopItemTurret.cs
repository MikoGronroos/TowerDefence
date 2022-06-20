using UnityEngine;

[CreateAssetMenu(menuName = "Shop Item/Turret")]
public class ShopItemTurret : ShopItemBarrack
{

    public Sprite TurretProjectileEffectIcon;

    public override void Buy() 
    {
        BuyAction();
    }

    public override void BuyAction()
    {
        base.BuyAction();
    }
}
