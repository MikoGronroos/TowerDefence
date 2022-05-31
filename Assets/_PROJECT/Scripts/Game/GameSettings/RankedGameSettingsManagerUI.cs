using Finark.Events;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankedGameSettingsManagerUI : MonoBehaviour
{
    [Header("Map Settings")]
    [SerializeField] private Image mapIcon;
    [SerializeField] private TextMeshProUGUI mapNameText;
    [SerializeField] private TextMeshProUGUI mapSkipsText;

    [SerializeField] private Button skipTheMapButton;

    [SerializeField] private RoomEventChannel roomEventChannel;

    private void Awake()
    {
        skipTheMapButton.onClick.AddListener(() => {
            MapManager.Instance.TryToChangeTheMap();
            UpdateMapSkipsText(PlayerManager.Instance.GetLocalPlayer().GetAmountOfMapSkips());
        });
    }

    private void Start()
    {
        UpdateMapSkipsText(PlayerManager.Instance.GetLocalPlayer().GetAmountOfMapSkips());
    }

    private void OnEnable()
    {
        roomEventChannel.OnMapChanged += DrawMap;
    }


    private void OnDisable()
    {
        roomEventChannel.OnMapChanged -= DrawMap;
    }

    private void UpdateMapSkipsText(int amount)
    {
        mapSkipsText.text = $"{amount} map skips left.";
    }

    private void DrawMap(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {
        var map = (Map)args["Map"];

        mapIcon.sprite = map.Icon;
        mapNameText.text = map.Name;
    }

}
