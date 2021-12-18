using UnityEngine;
using Photon.Pun;

public class GameSetup : MonoBehaviour
{

    [SerializeField] private GameObject playerPrefab;

    private PhotonView _photonView;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    private void Start()
    {

        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, Vector2.zero, Quaternion.identity);
        if (player.TryGetComponent(out LocalPlayer local))
        {

            PlayerManager.Instance.AddLocalPlayer(local);

            int amountOfPlayers = PhotonNetwork.PlayerList.Length - 1;

            _photonView.RPC("RPCValidatePlayer", RpcTarget.AllBuffered, amountOfPlayers);

            if (_photonView.IsMine)
            {
                local.IsLocal = true;
            }

            local.SetPlayerID(amountOfPlayers);

        }
    }

    [PunRPC]
    private void RPCValidatePlayer(int number)
    {

        GameManager.Instance.IncreaseAmountOfPlayerInTheRoom();

    }


}
