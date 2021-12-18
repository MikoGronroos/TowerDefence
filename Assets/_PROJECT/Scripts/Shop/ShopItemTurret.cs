using UnityEngine;

[CreateAssetMenu(menuName = "Shop Item/Turret")]
public class ShopItemTurret : ShopItem
{
    public override void Buy()
    {
        BuildingManager.Instance.SetBuilding(itemPrefab);
    }
}
