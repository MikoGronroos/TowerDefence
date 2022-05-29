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
    }

    private void OnDisable()
    {
        playerEventChannel.OnPlayerDead -= EndGame;
    }

    private void Start()
    {
        _photonView.RPC("SendLocalAccountData", RpcTarget.AllBuffered, PlayerManager.Instance.GetLocalPlayer().GetPlayerID(), AccountManager.Instance.CurrentAccount.AccountName);
    }

    [PunRPC]
    private void SendLocalAccountData(int id, string name)
    {
        roomEventChannel.OnPlayerInfoUpdate?.Invoke(new Dictionary<string, object> { 
            { "ID", id}, 
            { "Name", name } 
        });
    }

    public void StartChildCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }

    #region Game End

    private void EndGame(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {

        if (gameEnded) return;

        gameEnded = true;

        int loserID = (int)args["loserID"];

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
            AccountManager.Instance.CurrentAccount.CurrentTrophies = Mathf.Clamp(AccountManager.Instance.CurrentAccount.CurrentTrophies + 30, 0, int.MaxValue);
            if (AccountManager.Instance.CurrentAccount.CurrentTrophies > AccountManager.Instance.CurrentAccount.HighestTrophies)
            {
                AccountManager.Instance.CurrentAccount.HighestTrophies = AccountManager.Instance.CurrentAccount.CurrentTrophies;
            }
            playFabCurrencyEventChannel.ChangeAmountOfSoftCurrency?.Invoke(new Dictionary<string, object> { { "Amount", reward.SoftCurrencyReward } });
        }

        if (!IsWinner)
        {
            AccountManager.Instance.CurrentAccount.CurrentWinstreak = 0;
            AccountManager.Instance.CurrentAccount.CurrentTrophies = Mathf.Clamp(AccountManager.Instance.CurrentAccount.CurrentTrophies - 30, 0, int.MaxValue);
        }

        AccountManager.Instance.CurrentAccount.GamesPlayed++;
        AccountManager.Instance.SaveData();

        LoadGameEndScreen();

    }

    private void LoadGameEndScreen()
    {
        SceneManager.LoadScene("GameEndScreen", LoadSceneMode.Additive);

    }

    #endregion

}