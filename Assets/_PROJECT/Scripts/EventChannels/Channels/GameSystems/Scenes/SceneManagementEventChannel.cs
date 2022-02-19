using UnityEngine;

namespace Finark.Events
{
	[CreateAssetMenu(menuName = "EventChannels/SceneManagementEventChannel", fileName ="SceneManagementEventChannel")]
	public class SceneManagementEventChannel : EventChannelBase
	{

		public EventChannel UnloadScenes;

	}
}
