using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class GameSettingsManagerUI : MonoBehaviour
{

    [SerializeField] private Toggle singleplayerToggle;

    [Header("Map Settings")]
    [SerializeField] private Button decrementMapIndexButton;
    [SerializeField] private Button incrementMapIndexButton;

    [SerializeField] private Image mapIcon;
    [SerializeField] private TextMeshProUGUI mapNameText;

    private void Awake()
    {
        singleplayerToggle.onValueChanged.AddListener(delegate {
            GameSettingsManager.Instance.GetGameSettings().Singleplayer = singleplayerToggle.isOn;
        });

        decrementMapIndexButton.onClick.AddListener(() => {
            MapManager.Instance.ChangeMapIndex(-1);
        });
        incrementMapIndexButton.onClick.AddListener(() => {
            MapManager.Instance.ChangeMapIndex(1);
        });

    }

    private void OnEnable()
    {
        EventManager.SubscribeToEvent("OnMapChanged", DrawMap);
    }

    private void OnDisable()
    {
        EventManager.UnsubscribeToEvent("OnMapChanged", DrawMap);
    }


    private void DrawMap(Dictionary<string, object> args)
    {

        var map = (Map)args["Map"];

        mapIcon.sprite = map.Icon;
        mapNameText.text = map.Name;
    }

}
