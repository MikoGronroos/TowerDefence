using System.Collections.Generic;
using UnityEngine;

public class PvpMissionManager : MonoBehaviourSingleton<PvpMissionManager>
{

    [SerializeField] private List<PvpMission> currentMissions = new List<PvpMission>();

    [SerializeField] private List<PvpMission> completedMissions = new List<PvpMission>();
    [SerializeField] private List<PvpMission> failedMissions = new List<PvpMission>();

    [SerializeField] private List<PvpMission> missions = new List<PvpMission>();

    [SerializeField] private EventChannel missionCompletedChannel;
    [SerializeField] private EventChannel refreshMissionLogChannel;

    public void GetNewMissions(int amount)
    {

        if (missions.Count <= 0) return;

        for (int i = 0; i < amount; i++)
        {
            var quest = missions[UnityEngine.Random.Range(0, missions.Count)];
            quest.Load();
            currentMissions.Add(quest);
            missions.Remove(quest);
        }
        refreshMissionLogChannel.RaiseEvent(new Dictionary<string, object> { { "Missions", currentMissions } });
    }

    public void CompleteMission(PvpMission mission)
    {
        currentMissions.Remove(mission);
        completedMissions.Add(mission);
        mission.Unload();
        GiveMissionRewards(mission.MissionRewards);
        missionCompletedChannel.RaiseEvent(new Dictionary<string, object> { { "Mission", mission } });
        refreshMissionLogChannel.RaiseEvent(new Dictionary<string, object> { { "Missions", currentMissions } });
    }

    public void MissionFailed(PvpMission mission)
    {
        currentMissions.Remove(mission);
        failedMissions.Add(mission);
        mission.Unload();
        refreshMissionLogChannel.RaiseEvent(new Dictionary<string, object> { { "Missions", currentMissions } });
    }

    private void GiveMissionRewards(PvpMissionRewards rewards)
    {
        PlayerLevel.Instance.AddXp(rewards.XpReward);
        VirtualCurrencyManager.Instance.AddCurrency(rewards.MoneyReward);
    }

}
