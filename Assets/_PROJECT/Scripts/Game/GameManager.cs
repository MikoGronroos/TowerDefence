using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{

    //[SerializeField] private EventChannel gameEndedChannel;

    public void StartChildCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }

    #region Game End

    public void EndGame(int loserID)
    {
        //gameEndedChannel.RaiseEvent(new Dictionary<string, object> {{ "loserID", loserID }});
    }

    public void LeaveRoom()
    {
        RoomController.LeaveTheRoom();
    }

    #endregion

}
