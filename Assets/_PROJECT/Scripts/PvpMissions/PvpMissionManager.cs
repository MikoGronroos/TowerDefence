using System;
using System.Collections.Generic;
using UnityEngine;

public class PvpMissionManager : MonoBehaviourSingleton<PvpMissionManager>
{

    [SerializeField] private List<PvpMission> currentMissions = new List<PvpMission>();

    [SerializeField] private List<PvpMission> completedMissions = new List<PvpMission>();
    [SerializeField] private List<PvpMission> failedMissions = new List<PvpMission>();

    [SerializeField] private List<PvpMission> missions = new List<PvpMission>();

    private PvpMissionUI _missionUI;

    private void Awake()
    {
        _missionUI = GetComponent<PvpMissionUI>();
    }

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
        _missionUI.DrawPvpMissionsToLog(currentMissions);
    }

    public void CompleteMission(PvpMission mission)
    {
        currentMissions.Remove(mission);
        completedMissions.Add(mission);
        mission.Unload();
        GiveMissionRewards(mission.MissionRewards);
        _missionUI.MissionCompletedScreen(mission);
        _missionUI.DrawPvpMissionsToLog(currentMissions);
    }

    public void MissionFailed(PvpMission mission)
    {
        currentMissions.Remove(mission);
        failedMissions.Add(mission);
        mission.Unload();
        _missionUI.DrawPvpMissionsToLog(currentMissions);
    }

    private void GiveMissionRewards(PvpMissionRewards rewards)
    {
        PlayerLevel.Instance.AddXp(rewards.XpReward);
        VirtualCurrencyManager.Instance.AddCurrency(rewards.MoneyReward);
    }

}
