using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{

    [SerializeField] private int playerID;

    [SerializeField] private Path thisPath;

    //[SerializeField] private EventChannel pathSetupChannel;

    public int GetID()
    {
        return playerID;
    }

    public void OnPathManagerLoaded(Dictionary<string, object> args)
    {
        //pathSetupChannel.RaiseEvent(new Dictionary<string, object> { { "Path", thisPath } });
    }

}
