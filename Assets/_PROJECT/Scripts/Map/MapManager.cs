using UnityEngine;
using System.Collections.Generic;

public class MapManager : MonoBehaviourSingleton<MapManager>
{

    [SerializeField] private Map[] Maps;

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

        //GameStart.Instance.SceneToLoad = Maps[index].SceneName;
    }

}
