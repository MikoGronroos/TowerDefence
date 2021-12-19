
using UnityEngine;

[CreateAssetMenu(menuName = "Shop Item/Unit")]
public class ShopItemUnit : ShopItem
{

    public int IncomeAddonFromSpawning;

    public override void BuyAction()
    {
        VirtualCurrencyManager.Instance.AddIncome(IncomeAddonFromSpawning);
        UnitSpawner.Instance.SpawnUnit(ItemPrefab.name, PlayerManager.Instance.GetLocalPlayer().GetPlayerID());
    }
}
