using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HealthManager : MonoBehaviour
{

    [SerializeField] private List<PlayerHealth> playerHealths = new List<PlayerHealth>();

    private HealthManagerUI _healthManagerUI;

    private PhotonView _photonView;

    #region Singleton

    private static HealthManager _instance;

    public static HealthManager Instance
    {
        get
        {
            return _instance;
        }
    }

    #endregion

    private void Awake()
    {
        _instance = this;
        _healthManagerUI = GetComponent<HealthManagerUI>();
        _photonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        UpdateHealthOfEveryPlayer();
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
        _healthManagerUI.UpdateHealthText(player);

        if (PlayerHealthBelowZero(player))
        {
            GameManager.Instance.EndGame(id);
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
            _healthManagerUI.UpdateHealthText(player);
        }
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
