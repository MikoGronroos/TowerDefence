using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ChannelListener
{
    public EventChannel channel;

    //Invoke when channel heard
    public UnityEvent<Dictionary<string, object>> OnChannelHeardEvent;
}
