using Finark.Events;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerUI : MonoBehaviour
{

    [SerializeField] private RoomEventChannel roomEventChannel;

    [Header("Game End Panel")]

    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject defeatPanel;

    [SerializeField] private Button leaveTheRoomButton;

    private bool _pressedLeftButton = false;

    private void Awake()
    {
        leaveTheRoomButton.onClick.AddListener(() => {

            if (_pressedLeftButton) return;

            _pressedLeftButton = true;

            roomEventChannel?.LeaveRoom(null);
        });
    }

    private void Start()
    {
        Debug.Log("Start");
        GameEndScreen();
    }

    private void GameEndScreen()
    {

        if (GameManager.Instance.IsWinner)
        {
            victoryPanel.SetActive(true);
            Debug.Log("Victory");
        }
        else
        {
            Debug.Log("Defeat");
            defeatPanel.SetActive(true);
        }
    }

}
