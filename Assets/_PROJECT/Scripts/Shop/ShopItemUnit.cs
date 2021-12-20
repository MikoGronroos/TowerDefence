
using UnityEngine;

[CreateAssetMenu(menuName = "Shop Item/Unit")]
public class ShopItemUnit : ShopItem
{

    public int IncomeAddonFromSpawning;

    public int XpAddonWhenBought;

    public override void BuyAction()
    {
        VirtualCurrencyManager.Instance.AddIncome(IncomeAddonFromSpawning);
        PlayerLevel.Instance.AddXp(XpAddonWhenBought);
        UnitSpawner.Instance.SpawnUnit(ItemPrefab.name, PlayerManager.Instance.GetLocalPlayer().GetPlayerID());
    }
}
