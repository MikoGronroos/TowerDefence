using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PvpMissionUI : MonoBehaviour
{

    [SerializeField] private GameObject pvpMissionLog;
    [SerializeField] private Button pvpMissionLogButton;

    [SerializeField] private Transform pvpMissionLogContentParent;
    [SerializeField] private GameObject missionLogPrefab;

    private void Awake()
    {
        pvpMissionLogButton.onClick.AddListener(() => {
            TogglePvpMissionLog();
        });
    }

    public void TogglePvpMissionLog()
    {
        pvpMissionLog.SetActive(!pvpMissionLog.activeSelf);
    }

    public void DrawPvpMissionsToLog(List<PvpMission> currentMissions)
    {
        foreach (var mission in currentMissions)
        {
            GameObject newLog = Instantiate(missionLogPrefab, pvpMissionLogContentParent);
            var log = newLog.GetComponent<MissionLog>();
            log.SetupMissionLog(mission.Name, mission.Description, mission.Icon);
        }
    }

}
