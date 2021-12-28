public class GameManager : MonoBehaviourSingleton<GameManager>
{

    private GameManagerUI _gameManagerUI;

    private void Awake()
    {
        _gameManagerUI = GetComponent<GameManagerUI>();
    }

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
