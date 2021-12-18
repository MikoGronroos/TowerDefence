
using UnityEngine;

[CreateAssetMenu(menuName = "Shop Item/Unit")]
public class ShopItemUnit : ShopItem
{
    public override void Buy()
    {
        UnitSpawner.Instance.SpawnUnit(itemPrefab.name, PlayerManager.Instance.GetLocalPlayer().GetPlayerID());
    }
}
