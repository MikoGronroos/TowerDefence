using Finark.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameManager : MonoBehaviourSingleton<GameManager>
{

    [SerializeField] private PlayerEventChannel playerEventChannel;
    [SerializeField] private SceneManagementEventChannel sceneManagementEventChannel;
    [SerializeField] private PlayFabCurrencyEventChannel playFabCurrencyEventChannel;
    [SerializeField] private RoomEventChannel roomEventChannel;

    [SerializeField] private GameRewards reward;

    [SerializeField] private bool gameEnded = false;

    private PhotonView _photonView;

    public bool IsWinner { get; private set; }

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        playerEventChannel.OnPlayerDead += EndGame;
        roomEventChannel.OnPlayerSurrender += EndGame;
    }

    private void OnDisable()
    {
        playerEventChannel.OnPlayerDead -= EndGame;
        roomEventChannel.OnPlayerSurrender -= EndGame;
    }

    private void Start()
    {
        _photonView.RPC("SendLocalAccountData", RpcTarget.All, PlayerManager.Instance.GetLocalPlayer().GetPlayerID(),
            AccountManager.Instance.CurrentAccount.AccountName, 
            AccountManager.Instance.CurrentAccount.CurrentTrophies);
    }

    [PunRPC]
    private void SendLocalAccountData(int id, string name, int trophies)
    {
        roomEventChannel.OnPlayerInfoUpdate?.Invoke(new Dictionary<string, object> { 
            { "ID", id}, 
            { "Name", name },
            { "Trophies", trophies }
        });
    }

    #region Game End

    private void EndGame(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {
        _photonView.RPC("RPCEndGame", RpcTarget.AllBuffered, (int)args["loserID"]);
    }
    
    [PunRPC]
    private void RPCEndGame(int loserID)
    {
        if (gameEnded) return;

        gameEnded = true;

        IsWinner = loserID != PlayerManager.Instance.GetLocalPlayer().GetPlayerID();

        if (IsWinner)
        {
            AccountManager.Instance.CurrentAccount.CurrentWinstreak++;
            AccountManager.Instance.CurrentAccount.TotalVictories++;
            if (AccountManager.Instance.CurrentAccount.CurrentWinstreak > AccountManager.Instance.CurrentAccount.HighestWinstreak)
            {
                AccountManager.Instance.CurrentAccount.HighestWinstreak = AccountManager.Instance.CurrentAccount.CurrentWinstreak;
            }
            AccountManager.Instance.CurrentAccount.AccountXp += reward.ExperienceReward;
            AccountManager.Instance.CurrentAccount.CurrentTrophies = Mathf.Clamp(AccountManager.Instance.CurrentAccount.CurrentTrophies + 31, 0, int.MaxValue);
            if (AccountManager.Instance.CurrentAccount.CurrentTrophies > AccountManager.Instance.CurrentAccount.HighestTrophies)
            {
                AccountManager.Instance.CurrentAccount.HighestTrophies = AccountManager.Instance.CurrentAccount.CurrentTrophies;
            }
            playFabCurrencyEventChannel.ChangeAmountOfSoftCurrency?.Invoke(new Dictionary<string, object> { { "Amount", reward.SoftCurrencyReward } });
        }

        if (!IsWinner)
        {
            AccountManager.Instance.CurrentAccount.CurrentWinstreak = 0;
            AccountManager.Instance.CurrentAccount.CurrentTrophies = Mathf.Clamp(AccountManager.Instance.CurrentAccount.CurrentTrophies - 29, 0, int.MaxValue);
        }

        AccountManager.Instance.CurrentAccount.GamesPlayed++;
        AccountManager.Instance.SaveDataAccountData();

        LoadGameEndScreen();

    }

    private void LoadGameEndScreen()
    {
        SceneManager.LoadScene("GameEndScreen", LoadSceneMode.Additive);

    }

    #endregion

}