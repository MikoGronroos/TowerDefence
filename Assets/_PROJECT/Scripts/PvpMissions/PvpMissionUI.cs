using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PvpMissionUI : MonoBehaviour
{

    [SerializeField] private GameObject pvpMissionLog;
    [SerializeField] private Button pvpMissionLogButton;

    [SerializeField] private Transform pvpMissionLogContentParent;
    [SerializeField] private GameObject missionLogPrefab;

    [Header("Mission Completed")]

    [SerializeField] private PvpMissionCompletedUI pvpMissionCompletedUI;
    [SerializeField] private float onScreenTime;

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

    public void MissionCompletedScreen(PvpMission mission)
    {
        StartCoroutine(MissionCompleted(mission));
    }

    private IEnumerator MissionCompleted(PvpMission mission)
    {

        pvpMissionCompletedUI.SetupMissionCompletedUI(mission);

        pvpMissionCompletedUI.gameObject.SetActive(true);

        yield return new WaitForSeconds(onScreenTime);

        pvpMissionCompletedUI.gameObject.SetActive(false);

    }

}
