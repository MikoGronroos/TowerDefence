using System.Collections.Generic;
using UnityEngine;

public class EventChannelListener : MonoBehaviour
{

    [SerializeField] private List<ChannelListener> channelListeners = new List<ChannelListener>();

    public void OnEnable()
    {
        foreach (var e in channelListeners)
        {
            e.Channel.Callback += () => { e.OnChannelHeardEvent?.Invoke(e.Channel.Arguments); };
        }
    }

    private void OnDisable()
    {
        foreach (var e in channelListeners)
        {
            e.Channel.Callback -= () => { e.OnChannelHeardEvent?.Invoke(e.Channel.Arguments); };
        }
    }

}
