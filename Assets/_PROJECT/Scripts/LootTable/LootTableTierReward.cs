using UnityEngine;

[System.Serializable]
public class LootTableTierReward
{

    [Header("Currency Rewards")]
    public int HardCurrencyAmount;
    public int SoftCurrencyAmount;

    [Header("Account Rewards")]
    public int XpAmount;

    [Header("Skin Reward")]
    public int SkinID;
    public string SkinGraphicMainKey;

}