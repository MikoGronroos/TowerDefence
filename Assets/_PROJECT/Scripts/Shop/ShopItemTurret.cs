using UnityEngine;

[CreateAssetMenu(menuName = "Shop Item/Turret")]
public class ShopItemTurret : ShopItem
{
    public override void BuyAction()
    {
        BuildingManager.Instance.SetBuilding(ItemPrefab);
    }
}
