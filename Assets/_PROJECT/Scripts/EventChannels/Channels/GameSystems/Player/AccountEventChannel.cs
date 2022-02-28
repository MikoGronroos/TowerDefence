using UnityEngine;

namespace Finark.Events
{
	[CreateAssetMenu(menuName = "EventChannels/AccountEventChannel", fileName ="AccountEventChannel")]
	public class AccountEventChannel : EventChannelBase
	{

		public EventChannel OnNameChanged;

	}
}
