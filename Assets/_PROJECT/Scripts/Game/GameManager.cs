using Finark.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{

    [SerializeField] private PlayerEventChannel playerEventChannel;

    public void StartChildCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }

    #region Game End

    public void EndGame(int loserID)
    {
        playerEventChannel?.OnPlayerDead(new Dictionary<string, object> {{ "loserID", loserID }});
    }

    public void LeaveRoom()
    {
        RoomController.LeaveTheRoom();
    }

    #endregion

}
