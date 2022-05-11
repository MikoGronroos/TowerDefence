
using UnityEngine;

[CreateAssetMenu(menuName = "Shop Item/Unit")]
public class ShopItemUnit : ShopItem
{

    public float IncomeAddonFromSpawning;

    public int XpAddonWhenBought;

    public override void BuyAction()
    {
        UnitSpawner.Instance.RequestUnitSpawn(ItemPrefab.name, PlayerManager.Instance.GetLocalPlayer().GetPlayerID());
        VirtualCurrencyManager.Instance.AddIncome(IncomeAddonFromSpawning);
        PlayerLevel.Instance.AddXp(XpAddonWhenBought);
    }
}
