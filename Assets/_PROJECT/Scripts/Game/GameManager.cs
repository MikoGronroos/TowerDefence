using System.Collections;

public class GameManager : MonoBehaviourSingleton<GameManager>
{

    private GameManagerUI _gameManagerUI;

    private void Awake()
    {
        _gameManagerUI = GetComponent<GameManagerUI>();
    }

    public void StartChildCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
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
