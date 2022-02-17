using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EventChannel")]
public class EventChannel : ScriptableObject
{

    public Action Callback;

    public Dictionary<string, object> Arguments { get; private set; }

    //Happens from where you get the data
    public void RaiseEvent(Dictionary<string, object> args)
    {
        Arguments = args;
        Callback?.Invoke();
    }

}
