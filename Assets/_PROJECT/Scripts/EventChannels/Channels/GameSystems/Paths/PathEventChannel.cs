using Finark.Events;
using UnityEngine;

namespace Finark.Events
{
    [CreateAssetMenu(menuName = "EventChannels/PathEventChannel", fileName = "PathEventChannel")]
    public class PathEventChannel : EventChannelBase
    {
        public EventChannel SetupPaths;
    }
}
