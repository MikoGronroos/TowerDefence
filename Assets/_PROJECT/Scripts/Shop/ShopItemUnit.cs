
using UnityEngine;

[CreateAssetMenu(menuName = "Shop Item/Unit")]
public class ShopItemUnit : ShopItem
{
    public override void BuyAction()
    {
        UnitSpawner.Instance.SpawnUnit(ItemPrefab.name, PlayerManager.Instance.GetLocalPlayer().GetPlayerID());
    }
}
