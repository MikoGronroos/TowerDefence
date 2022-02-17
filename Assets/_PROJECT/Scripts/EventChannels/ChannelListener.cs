using System.Collections.Generic;
using UnityEngine.Events;

[System.Serializable]
public class ChannelListener
{

    public EventChannel Channel;

    //Invoke when channel heard
    public UnityEvent<Dictionary<string, object>> OnChannelHeardEvent;

}
