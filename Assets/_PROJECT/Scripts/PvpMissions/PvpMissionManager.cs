using System;
using System.Collections.Generic;
using UnityEngine;

public class PvpMissionManager : MonoBehaviourSingleton<PvpMissionManager>
{

    [SerializeField] private List<PvpMission> currentMissions = new List<PvpMission>();

    [SerializeField] private List<PvpMission> completedMissions = new List<PvpMission>();
    [SerializeField] private List<PvpMission> failedMissions = new List<PvpMission>();

    [SerializeField] private List<PvpMission> missions = new List<PvpMission>();

    public void GetNewMissions(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            var quest = missions[UnityEngine.Random.Range(0, missions.Count)];
            currentMissions.Add(quest);
            quest.Load();
        }
    }

    public void CompleteMission(PvpMission mission)
    {
        currentMissions.Remove(mission);
        completedMissions.Add(mission);
        mission.Unload();
    }

    public void MissionFailed(PvpMission mission)
    {
        currentMissions.Remove(mission);
        failedMissions.Add(mission);
        mission.Unload();
    }

}
