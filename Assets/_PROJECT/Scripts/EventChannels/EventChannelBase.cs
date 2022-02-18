using System;
using System.Collections.Generic;
using UnityEngine;

namespace Finark.Events
{
    public abstract class EventChannelBase : ScriptableObject
    {

        public delegate void EventChannel(Dictionary<string, object> args, Action callback = null);

    }
}
