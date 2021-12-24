using System;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{

    private static Dictionary<string, Action<Dictionary<string, object>>> events = new Dictionary<string, Action<Dictionary<string, object>>>();

    public static void CreateEvent(string name)
    {
        if (CheckIfDictionaryContainsKey(name))
        {
            Debug.LogError($"{name}: is already a key in events dictionary!");
            return;
        }

        events.Add(name, null);

    }

    public static void InvokeEvent(string name, Dictionary<string, object> message)
    {
        if (CheckIfDictionaryContainsKey(name))
        {
            events[name]?.Invoke(message);
        }
    }

    public static void SubscribeToEvent(string name, Action<Dictionary<string, object>> listener)
    {
        if (CheckIfDictionaryContainsKey(name))
        {
            events[name] += listener;
        }
    }

    public static void UnsubscribeToEvent(string name, Action<Dictionary<string, object>> listener)
    {
        if (CheckIfDictionaryContainsKey(name))
        {
            events[name] -= listener;
        }
    }

    private static bool CheckIfDictionaryContainsKey(string name)
    {
        return events.ContainsKey(name);
    }

}
