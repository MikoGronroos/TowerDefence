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

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            _photonView.RPC("RPCChangeMapIndex", RpcTarget.AllBuffered, GetRandomMapIndex());
        }
    }

    public void ChangeMapIndexByOne(int amount)
    {
        _photonView.RPC("RPCChangeMapIndexByOne", RpcTarget.AllBuffered, amount);
    }

    public void TryToChangeTheMap()
    {
        if (PlayerManager.Instance.GetLocalPlayer().GetAmountOfMapSkips() > 0)
        {
            PlayerManager.Instance.GetLocalPlayer().SetAmountOfMapSkips(PlayerManager.Instance.GetLocalPlayer().GetAmountOfMapSkips() - 1);
            int index = GetRandomMapIndex();
            _photonView.RPC("RPCChangeMapIndex", RpcTarget.AllBuffered, index);
        }
    }

    [PunRPC]
    private void RPCChangeMapIndexByOne(int amount)
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

    [PunRPC]
    private void RPCChangeMapIndex(int index)
    {
        MapIndexChanged(index);
    }

    private void MapIndexChanged(int index)
    {
        GameSettingsManager.Instance.GetGameSettings().CurrentMapIndex = index;

        roomEventChannel.OnMapChanged?.Invoke(new Dictionary<string, object> { { "Map", Maps[index] } });
    }

    private int GetRandomMapIndex()
    {
        return Random.Range(0, Maps.Length);
    }

}
