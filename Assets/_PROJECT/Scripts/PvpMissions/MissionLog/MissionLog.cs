using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissionLog : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI missionTitle;
    [SerializeField] private TextMeshProUGUI missionDescription;

    [SerializeField] private Image missionIcon;

    public void SetupMissionLog(string title, string description, Sprite icon)
    {
        missionTitle.text = title;
        missionDescription.text = description;

        missionIcon.sprite = icon;
    }

}
