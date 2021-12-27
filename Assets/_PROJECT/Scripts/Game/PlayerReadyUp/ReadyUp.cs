using UnityEngine;
using Photon.Pun;

public class ReadyUp : MonoBehaviour
{

    private PhotonView _photonView;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    public void LocalPlayerReadyUp()
    {

        var localPlayer = PlayerManager.Instance.GetLocalPlayer();

        if (!localPlayer.IsPlayerReady())
        {
            _photonView.RPC("RPCLocalPlayerReadyUp", RpcTarget.AllBuffered);
            localPlayer.TogglePlayerIsReady(true);
        }
    }

    [PunRPC]
    private void RPCLocalPlayerReadyUp()
    {
        GameStart.Instance.IncreaseAmountOfReadyPlayers();
    }

}
