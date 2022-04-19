using UnityEngine;

public abstract class ShopItem : ScriptableObject
{

    public int Cost;
    public string IconMainKey;

    public int LevelToUnlock;

    public GameObject ItemPrefab;

    public virtual void Buy()
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
