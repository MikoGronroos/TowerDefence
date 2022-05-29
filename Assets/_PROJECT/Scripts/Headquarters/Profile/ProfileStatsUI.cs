using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProfileStatsUI : MonoBehaviour
{

	[SerializeField] private Button profileTabButton;

    [SerializeField] private TextMeshProUGUI profileNameText;

    [SerializeField] private TextMeshProUGUI profileLevelText;

    [SerializeField] private TextMeshProUGUI profileCurrentWinstreakText;
    [SerializeField] private TextMeshProUGUI profileHighestWinstreakText;

    [SerializeField] private TextMeshProUGUI profileTotalVictoriesText;

    [SerializeField] private TextMeshProUGUI profileCurrentTrophiesText;
    [SerializeField] private TextMeshProUGUI profileHighestTrophiesText;

    [SerializeField] private TextMeshProUGUI profileGamesPlayedText;

    private void Awake()
    {
        profileTabButton.onClick.AddListener(UpdateStats);
    }

    private void UpdateStats()
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

}
