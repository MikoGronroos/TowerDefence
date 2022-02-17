using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EventChannel")]
public class EventChannel : ScriptableObject
{

    public Action<Dictionary<string, object>> Callback;

    //Happens from where you get the data
    public void RaiseEvent(Dictionary<string, object> args)
    {
        Callback?.Invoke(args);
    }

}
