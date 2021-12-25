using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PvpMissionUI : MonoBehaviour
{

    [SerializeField] private GameObject pvpMissionLog;
    [SerializeField] private Button pvpMissionLogButton;

    [SerializeField] private Transform pvpMissionLogContentParent;
    [SerializeField] private GameObject missionLogPrefab;

    private List<GameObject> _drawnMissionLogs = new List<GameObject>();

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

        EraseDrawnMissionLogs();

        foreach (var mission in currentMissions)
        {
            GameObject newLog = Instantiate(missionLogPrefab, pvpMissionLogContentParent);
            var log = newLog.GetComponent<MissionLog>();
            log.SetupMissionLog(mission.Name, mission.Description, mission.Icon);
            _drawnMissionLogs.Add(newLog);
        }
    }

    private void EraseDrawnMissionLogs()
    {
        foreach (var log in _drawnMissionLogs)
        {
            Destroy(log);
        }
        _drawnMissionLogs.Clear();
    }

}
