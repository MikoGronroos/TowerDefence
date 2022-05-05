using Finark.Events;
using System.Collections.Generic;
using UnityEngine;

public class PvpMissionManager : MonoBehaviourSingleton<PvpMissionManager>
{

    [SerializeField] private List<PvpMission> currentMissions = new List<PvpMission>();

    [SerializeField] private List<PvpMission> completedMissions = new List<PvpMission>();
    [SerializeField] private List<PvpMission> failedMissions = new List<PvpMission>();

    [SerializeField] private List<PvpMission> missions = new List<PvpMission>();

    [SerializeField] private PlayerEventChannel playerEventChannel;

    public void GetNewMissions(int amount)
    {

        if (missions.Count <= 0) return;

        for (int i = 0; i < amount; i++)
        {
            var quest = missions[Random.Range(0, missions.Count)];
            quest.Load();
            currentMissions.Add(quest);
            missions.Remove(quest);
        }
        playerEventChannel?.RefreshMissionLog(new Dictionary<string, object> { { "Missions", currentMissions } });
    }

    public void CompleteMission(PvpMission mission)
    {
        currentMissions.Remove(mission);
        completedMissions.Add(mission);
        mission.Unload();
        GiveMissionRewards(mission.MissionRewards);
        SoundEffectManager.Instance.PlaySoundInstantlyWithID("QuestCompleted");
        playerEventChannel?.OnMissionCompleted(new Dictionary<string, object> { { "Mission", mission } });
        playerEventChannel?.RefreshMissionLog(new Dictionary<string, object> { { "Missions", currentMissions } });
    }

    public void MissionFailed(PvpMission mission)
    {
        currentMissions.Remove(mission);
        failedMissions.Add(mission);
        mission.Unload();
        playerEventChannel?.RefreshMissionLog(new Dictionary<string, object> { { "Missions", currentMissions } });
    }

    private void GiveMissionRewards(PvpMissionRewards rewards)
    {
        PlayerLevel.Instance.AddXp(rewards.XpReward);
        VirtualCurrencyManager.Instance.AddCurrency(rewards.MoneyReward);
    }

}
