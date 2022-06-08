using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Finark.Events;

public class HealthManager : MonoBehaviourSingleton<HealthManager>
{

    [SerializeField] private List<PlayerHealth> playerHealths = new List<PlayerHealth>();

    [SerializeField] private PlayerEventChannel playerEventChannel;

    private PhotonView _photonView;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    public void RemoveHealthWithID(int amount, int id)
    {

        var player = GetPlayerHealthWithID(id);
        player.Health = Mathf.Clamp(player.Health -= amount, 0, player.Health);

        _photonView.RPC("RPCRemoveHealthWithID", RpcTarget.AllBuffered, player.Health, id);

        if (PlayerHealthBelowZero(player))
        {
            playerEventChannel?.OnPlayerDead(new Dictionary<string, object> { { "loserID", id } });
        }

    }

    [PunRPC]
    private void RPCRemoveHealthWithID(int amount, int id)
    {
        playerEventChannel?.OnHealthChanged(new Dictionary<string, object> { { "ID", id }, { "amount", amount } });
    }

    private bool PlayerHealthBelowZero(PlayerHealth player)
    {
        return player.Health <= 0;
    }

    public void UpdateHealthOfEveryPlayer()
    {
        foreach (var player in playerHealths)
        {
            playerEventChannel?.OnHealthChanged(new Dictionary<string, object> { { "ID", player.PlayerID }, { "amount", player.Health } });
        }
    }

    public void SetHealhtOfEveryPlayer(int value)
    {
        foreach (var player in playerHealths)
        {
            player.Health = value;
        }
        UpdateHealthOfEveryPlayer();
    }

    private PlayerHealth GetPlayerHealthWithID(int id)
    {

        PlayerHealth health = null;

        foreach (var item in playerHealths)
        {
            if (item.PlayerID == id)
            {
                health = item;
            }
        }

        return health;

    }


}
