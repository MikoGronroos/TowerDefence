using UnityEngine;
using TMPro;

public class PvpMissionCompletedUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI missionNameText;
    [SerializeField] private TextMeshProUGUI goldRewardText;
    [SerializeField] private TextMeshProUGUI xpRewardText;

    [SerializeField] private string suffixGoldReward;
    [SerializeField] private string suffixXpReward;

    public void SetupMissionCompletedUI(PvpMission mission)
    {
        missionNameText.text = mission.Name;
        goldRewardText.text = $"{mission.MissionRewards.MoneyReward} {suffixGoldReward}";
        xpRewardText.text = $"{mission.MissionRewards.XpReward} {suffixXpReward}";
    }

}
