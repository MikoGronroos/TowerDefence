using UnityEngine;

namespace Finark.Events
{
	[CreateAssetMenu(menuName = "EventChannels/ServerEventChannel", fileName ="ServerEventChannel")]
	public class ServerEventChannel : EventChannelBase
	{

		public EventChannel OnLobbyJoined;
		public EventChannel OnPlayfabLogin;
		public EventChannel OnAccountDataFetched;

	}
}
