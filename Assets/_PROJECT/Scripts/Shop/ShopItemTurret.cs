using UnityEngine;

[CreateAssetMenu(menuName = "Shop Item/Turret")]
public class ShopItemTurret : ShopItem
{

    public override void Buy() 
    {
        BuyAction();
    }

    public override void BuyAction()
    {
        BuildingManager.Instance.SetBuilding(ItemPrefab, Cost);
    }
}
