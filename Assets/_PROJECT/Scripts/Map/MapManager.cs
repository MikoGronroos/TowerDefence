using UnityEngine;
using System.Collections.Generic;
using Finark.Events;

public class MapManager : MonoBehaviourSingleton<MapManager>
{

    [SerializeField] private Map[] Maps;

    [SerializeField] private RoomEventChannel roomEventChannel; 

    private int _currentMapIndex = 0;

    private const int _defaultMapIndex = 0;

    private void Start()
    {
        MapIndexChanged(_defaultMapIndex);
    }

    public void ChangeMapIndex(int amount)
    {

        _currentMapIndex += amount;

        if (_currentMapIndex < 0)
        {
            _currentMapIndex = Maps.Length - 1;
        }
        else if (_currentMapIndex > Maps.Length - 1)
        {
            _currentMapIndex = 0;
        }

        MapIndexChanged(_currentMapIndex);

    }

    private void MapIndexChanged(int index)
    {
        GameSettingsManager.Instance.GetGameSettings().CurrentMapIndex = index;

        EventManager.InvokeEvent("OnMapChanged", new Dictionary<string, object>
        {
            { "Map", Maps[index]}
        });

        roomEventChannel.OnMapChanged?.Invoke(new Dictionary<string, object> { { "MapName", Maps[index].SceneName } });
    }

}
