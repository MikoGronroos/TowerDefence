using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI amountOfPlayersReadyText;

    [Header("Game End Screen")]

    [SerializeField] private GameObject gameEndScreen;
    [SerializeField] private TextMeshProUGUI winnerText;
    [SerializeField] private Button leaveTheRoomButton;

    private void Awake()
    {
        leaveTheRoomButton.onClick.AddListener(() => {
            GameManager.Instance.LeaveRoom();
        });
    }

    public void UpdateAmountOfPlayersText(int currentlyReady, int playersInTheGame) => amountOfPlayersReadyText.text = $"{currentlyReady}/{playersInTheGame}";

    public void GameEndedUI(int loserID)
    {
        gameEndScreen.SetActive(true);

        string gameEndText;

        if (PlayerManager.Instance.GetLocalPlayer().GetPlayerID() != loserID)
        {
            gameEndText = "You won";
        }
        else
        {
            gameEndText = "You lost";
        }
        winnerText.text = gameEndText;
    }

}