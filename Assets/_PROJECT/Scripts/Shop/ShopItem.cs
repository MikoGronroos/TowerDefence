using UnityEngine;

public abstract class ShopItem : ScriptableObject
{

    public int Cost;
    public Sprite Icon;

    public GameObject ItemPrefab;

    public void Buy()
    {
        if (VitrualCurrencyManager.Instance.CheckIfPlayerHasEnoughCurrency(Cost))
        {
            VitrualCurrencyManager.Instance.RemoveCurrency(Cost);
            BuyAction();
        }
        else
        {
            return;
        }
    }

    public abstract void BuyAction();
}
