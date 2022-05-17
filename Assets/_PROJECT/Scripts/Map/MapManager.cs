using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;
using Finark.Events;

public class MapManager : MonoBehaviourSingleton<MapManager>
{

    [SerializeField] private Map[] Maps;

    [SerializeField] private RoomEventChannel roomEventChannel; 

    private int _currentMapIndex = 0;

    private PhotonView _photonView;

    private const int _defaultMapIndex = 0;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        MapIndexChanged(_defaultMapIndex);
    }

    public void ChangeMapIndex(int amount)
    {
        _photonView.RPC("RPCChangeMapIndex", RpcTarget.AllBuffered, amount);
    }

    [PunRPC]
    private void RPCChangeMapIndex(int amount)
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

        roomEventChannel.OnMapChanged?.Invoke(new Dictionary<string, object> { { "Map", Maps[index] } });
    }

}
