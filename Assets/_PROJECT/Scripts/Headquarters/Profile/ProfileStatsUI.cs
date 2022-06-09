using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Finark.Events;
using System.Collections.Generic;
using System;

public class ProfileStatsUI : MonoBehaviour
{

    [Header("Small User Info")]

    [SerializeField] private TextMeshProUGUI smallProfileLevelText;
    [SerializeField] private TextMeshProUGUI smallProfileUsernameText;

    [Header("Large User Info")]

	[SerializeField] private Button profileTabButton;

    [SerializeField] private TextMeshProUGUI profileNameText;

    [SerializeField] private TextMeshProUGUI profileLevelText;

    [SerializeField] private TextMeshProUGUI profileCurrentWinstreakText;
    [SerializeField] private TextMeshProUGUI profileHighestWinstreakText;

    [SerializeField] private TextMeshProUGUI profileTotalVictoriesText;

    [SerializeField] private TextMeshProUGUI profileCurrentTrophiesText;
    [SerializeField] private TextMeshProUGUI profileHighestTrophiesText;

    [SerializeField] private TextMeshProUGUI profileGamesPlayedText;

    [SerializeField] private AccountEventChannel accountEventChannel;

    private void Awake()
    {
        profileTabButton.onClick.AddListener(UpdateLargeProfileInfoStats);
    }

    private void Start()
    {
        UpdateSmallProfileInfoStats();
    }

    private void OnEnable()
    {
        accountEventChannel.OnNameChanged += UpdateSmallProfileInfoStats;
    }

    private void OnDisable()
    {
        accountEventChannel.OnNameChanged -= UpdateSmallProfileInfoStats;
    }

    private void UpdateLargeProfileInfoStats()
    {

        profileNameText.text = AccountManager.Instance.CurrentAccount.AccountName;

        profileLevelText.text = $"LV <#43536c><size=170%>{AccountManager.Instance.CurrentAccount.AccountLevel}";

        profileCurrentWinstreakText.text = AccountManager.Instance.CurrentAccount.CurrentWinstreak.ToString();
        profileHighestWinstreakText.text = AccountManager.Instance.CurrentAccount.HighestWinstreak.ToString();

        profileTotalVictoriesText.text = AccountManager.Instance.CurrentAccount.TotalVictories.ToString();

        profileCurrentTrophiesText.text = AccountManager.Instance.CurrentAccount.CurrentTrophies.ToString();
        profileHighestTrophiesText.text = AccountManager.Instance.CurrentAccount.HighestTrophies.ToString();

        profileGamesPlayedText.text = AccountManager.Instance.CurrentAccount.GamesPlayed.ToString();

    }

    private void UpdateSmallProfileInfoStats(Dictionary<string, object> args = null, Action<Dictionary<string, object>> callback = null)
    {
        smallProfileLevelText.text = AccountManager.Instance.CurrentAccount.AccountLevel.ToString();
        smallProfileUsernameText.text = args != null ? (string)args["Name"] : AccountManager.Instance.CurrentAccount.AccountName;
    }

}
