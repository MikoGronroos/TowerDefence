using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Finark.Events;
using System;

public class GameSettingsManagerUI : MonoBehaviour
{

    [SerializeField] private Toggle singleplayerToggle;

    [Header("Map Settings")]
    [SerializeField] private Button decrementMapIndexButton;
    [SerializeField] private Button incrementMapIndexButton;

    [SerializeField] private Image mapIcon;
    [SerializeField] private TextMeshProUGUI mapNameText;

    [SerializeField] private RoomEventChannel roomEventChannel;

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
        roomEventChannel.OnMapChanged += DrawMap;
    }


    private void OnDisable()
    {
        roomEventChannel.OnMapChanged -= DrawMap;
    }


    private void DrawMap(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {
        var map = (Map)args["Map"];

        mapIcon.sprite = map.Icon;
        mapNameText.text = map.Name;
    }

}
