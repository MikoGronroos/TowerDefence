using UnityEngine;
using Photon.Pun;

public class GameSetup : MonoBehaviour
{

    [SerializeField] private GameObject playerPrefab;

    [SerializeField] private GameSettings settings;

    private PhotonView _photonView;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    private void Start()
    {

        #region SpawnLocalPlayer

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

        #endregion

        VirtualCurrencyManager.Instance.SetCurrency(settings.StartingCurrency);
        VirtualCurrencyManager.Instance.SetIncome(settings.StartingIncome);
        VirtualCurrencyManager.Instance.SetInterval(settings.IncomeInterval);

        HealthManager.Instance.SetHealhtOfEveryPlayer(settings.StartingHealth);
        PlayerLevel.Instance.SetLevel(settings.StartingLevel);

    }

    [PunRPC]
    private void RPCValidatePlayer(int number)
    {

        GameManager.Instance.IncreaseAmountOfPlayerInTheRoom();

    }


}
