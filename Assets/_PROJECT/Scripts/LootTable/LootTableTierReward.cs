using UnityEngine;

[System.Serializable]
public class LootTableTierReward
{

    public RewardType Type;

    [Header("Currency Rewards")]
    public int SoftCurrencyAmount;
    public int HardCurrencyAmount;

    [Header("Account Rewards")]
    public int XpAmount;

    [Header("Skin Reward")]
    public int SkinID;
    public string SkinGraphicMainKey;

}

public enum RewardType
{
    SoftCurrency,
    HardCurrency,
    Skin
}
