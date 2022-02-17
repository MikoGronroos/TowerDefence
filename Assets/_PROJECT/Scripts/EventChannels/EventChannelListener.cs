using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventChannelListener : MonoBehaviour
{

    [SerializeField] private List<ChannelListener> channelListeners = new List<ChannelListener>();

    private int index = 0;

    public void OnEnable()
    {
        index = 0;
        foreach (var e in channelListeners)
        {
            e.channel.Callback += OnChannelHeard;
            index++;
        }
    }

    private void OnDisable()
    {
        foreach (var e in channelListeners)
        {
            e.channel.Callback -= OnChannelHeard;
        }
    }

    private void OnChannelHeard(Dictionary<string, object> args)
    {
        channelListeners[index].OnChannelHeardEvent?.Invoke(args);
    }

}
