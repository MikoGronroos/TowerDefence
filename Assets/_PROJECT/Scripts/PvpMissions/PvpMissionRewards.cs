using UnityEngine;

[CreateAssetMenu(menuName = "Pvp Mission/Mission Rewards")]
public class PvpMissionRewards : ScriptableObject
{

    public PvpMissionRewards(int XpReward, int MoneyReward)
    {
        this.XpReward = XpReward;
        this.MoneyReward = MoneyReward;
    }

    public int XpReward;
    public int MoneyReward;

}
