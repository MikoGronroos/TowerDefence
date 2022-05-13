using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProfileStatsUI : MonoBehaviour
{

	[SerializeField] private Button profileTabButton;

    [SerializeField] private TextMeshProUGUI profileNameText;
    [SerializeField] private TextMeshProUGUI profileLevelText;
    [SerializeField] private TextMeshProUGUI profileWinstreakText;
    [SerializeField] private TextMeshProUGUI profileGamesPlayedText;

    private void Awake()
    {
        profileTabButton.onClick.AddListener(UpdateStats);
    }

    private void UpdateStats()
    {

        profileNameText.text = AccountManager.Instance.CurrentAccount.AccountName;
        profileLevelText.text = AccountManager.Instance.CurrentAccount.AccountLevel.ToString();
        profileWinstreakText.text = AccountManager.Instance.CurrentAccount.Winstreak.ToString();
        profileGamesPlayedText.text = AccountManager.Instance.CurrentAccount.GamesPlayed.ToString();

    }

}
