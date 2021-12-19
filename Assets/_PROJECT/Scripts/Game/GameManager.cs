using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{

    [SerializeField] private int currentlyReadyPlayers = 0;
    [SerializeField] private int playersInTheRoom = 0;

    private GameManagerUI _gameManagerUI;

    private void Awake()
    {
        _gameManagerUI = GetComponent<GameManagerUI>();
    }

    #region Player Amounts

    public void IncreaseAmountOfPlayerInTheRoom()
    {
        playersInTheRoom++;
        _gameManagerUI.UpdateAmountOfPlayersText(currentlyReadyPlayers, playersInTheRoom);
    }

    public void DecreaseAmountOfPlayerInTheRoom()
    {
        playersInTheRoom--;
        _gameManagerUI.UpdateAmountOfPlayersText(currentlyReadyPlayers, playersInTheRoom);
    }

    public void IncreaseAmountOfReadyPlayers()
    { 
        currentlyReadyPlayers++;
        _gameManagerUI.UpdateAmountOfPlayersText(currentlyReadyPlayers, playersInTheRoom);
    }

    public void DecreaseAmountOfReadyPlayers()
    {
        currentlyReadyPlayers--;
        _gameManagerUI.UpdateAmountOfPlayersText(currentlyReadyPlayers, playersInTheRoom);
    }

    public int GetAmountOfPlayersInTheRoom()
    {
        return playersInTheRoom;
    }

    #endregion

    #region Game End

    public void EndGame(int loserID)
    {
        _gameManagerUI.GameEndedUI(loserID);
    }

    public void LeaveRoom()
    {
        RoomController.LeaveTheRoom();
    }

    #endregion

}
