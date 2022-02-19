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

        _photonView.RPC("RPCRemoveHealthWithID", RpcTarget.AllBuffered, amount, id);

    }

    [PunRPC]
    private void RPCRemoveHealthWithID(int amount, int id)
    {
        var player = GetPlayerHealthWithID(id);
        player.Health = Mathf.Clamp(player.Health -= amount, 0, player.Health);
        playerEventChannel?.OnHealthChanged(new Dictionary<string, object> { { "PlayerHealth", player } });

        if (PlayerHealthBelowZero(player))
        {
            playerEventChannel?.OnPlayerDead(new Dictionary<string, object> { { "loserID", id} });
        }

    }

    private bool PlayerHealthBelowZero(PlayerHealth player)
    {
        return player.Health <= 0;
    }

    public void UpdateHealthOfEveryPlayer()
    {
        foreach (var player in playerHealths)
        {
            playerEventChannel?.OnHealthChanged(new Dictionary<string, object> { { "PlayerHealth", player } });
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
