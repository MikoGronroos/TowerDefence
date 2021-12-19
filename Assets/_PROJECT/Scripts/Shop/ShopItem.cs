using UnityEngine;

public abstract class ShopItem : ScriptableObject
{

    public int Cost;
    public Sprite Icon;

    public GameObject ItemPrefab;

    public void Buy()
    {
        if (VirtualCurrencyManager.Instance.CheckIfPlayerHasEnoughCurrency(Cost))
        {
            VirtualCurrencyManager.Instance.RemoveCurrency(Cost);
            BuyAction();
        }
        else
        {
            return;
        }
    }

    public abstract void BuyAction();
}
